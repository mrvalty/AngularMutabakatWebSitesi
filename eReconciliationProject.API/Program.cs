using Autofac;
using Autofac.Extensions.DependencyInjection;
using eReconciliationProject.Business.Autofac;
using eReconciliationProject.Business.Concrete;
using eReconciliationProject.Core.DependencyRevolvers;
using eReconciliationProject.Core.Extensions;
using eReconciliationProject.Core.Utilities.Ioc;
using eReconciliationProject.Core.Utilities.Security.Encryption;
using eReconciliationProject.Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CurrencyAccountManager>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule()));

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddMemoryCache();
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

builder.Services.AddHttpContextAccessor();

builder.Services.AddDependencyResolvers(new ICoreModule[]{
    new CoreModule()
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyOrigin());
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
