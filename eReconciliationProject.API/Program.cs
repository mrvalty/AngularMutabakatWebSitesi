using Autofac;
using Autofac.Extensions.DependencyInjection;
using eReconciliationProject.Business.Autofac;
using eReconciliationProject.Core.DependencyRevolvers;
using eReconciliationProject.Core.Utilities.Ioc;
using eReconciliationProject.Core.Utilities.Security.Encryption;
using eReconciliationProject.Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.IdentityModel.Tokens;
using eReconciliationProject.Core.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

// Add services to the container.
IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:7256")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();

        });
});
var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.Securitykey)
    };
});

builder.Services.AddDependencyResolvers(new ICoreModule[]{
    new CoreModule()
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
