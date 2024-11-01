using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Concrete;
using eReconciliationProject.Core.Utilities.Security.JWT;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.DA.Repositories.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace eReconciliationProject.Business.Autofac
{
    public static class AutofacBusinessModule
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepo>();
            services.AddScoped<ICompanyService, CompanyManager>();

            services.AddScoped<IAccountReconciliatonRepository, AccountReconciliatonRepo>();
            services.AddScoped<IAccountReconciliationService, AccountReconciliationManager>();

            services.AddScoped<IAccountReconciliationDetailRepository, AccountReconciliationDetailRepo>();
            services.AddScoped<IAccountReconciliationDetailService, AccountReconciliationDetailManager>();

            services.AddScoped<IBaBsReconciliationDetailRepository, BaBsReconciliationDetailRepo>();
            services.AddScoped<IBaBsReconciliationDetailService, BaBsReconciliationDetailManager>();

            services.AddScoped<IBaBsReconciliationRepository, BaBsReconciliationRepo>();
            services.AddScoped<IBaBsReconciliationService, BaBsReconciliationManager>();

            services.AddScoped<ICurrencyAccountRepository, CurrencyAccountRepo>();
            services.AddScoped<ICurrencyAccountService, CurrencyAccountManager>();

            services.AddScoped<ICurrencyRepository, CurrencyRepo>();
            services.AddScoped<ICurrencyService, CurrencyManager>();

            services.AddScoped<IMailParameterRepository, MailParameterRepo>();
            services.AddScoped<IMailParameterService, MailParameterManager>();

            services.AddScoped<IMailRepository, MailRepo>();
            services.AddScoped<IMailService, MailManager>();

            services.AddScoped<IMailTemplateRepository, MailTemplateRepo>();
            services.AddScoped<IMailTemplateService, MailTemplateManager>();

            services.AddScoped<IMailParameterRepository, MailParameterRepo>();
            services.AddScoped<IMailParameterService, MailParameterManager>();

            services.AddScoped<IUserRepository, UserRepo>();
            services.AddScoped<IUserService, UserManager>();

            services.AddScoped<IOperationClaimRepository, OperationClaimRepo>();
            services.AddScoped<IOperationClaimService, OperationClaimManager>();

            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<IAuthService, AuthManager>();

            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepo>();
            services.AddScoped<IUserOperationClaimService, UserOperationClaimManager>();

            services.AddScoped<ITermsandConditionsRepository, TermsandConditionRepo>();
            services.AddScoped<ITermsandConditionService, TermsandCoditionManager>();

            services.AddScoped<IUserForgotPasswordRepository, UserForgotPasswordRepo>();
            services.AddScoped<IUserForgotPasswordService, UserForgotPasswordManager>();

            services.AddScoped<IUserRelationshipRepository, UserRelationshipRepo>();
            services.AddScoped<IUserRelationshipService, UserRelationshipManager>();

            services.AddScoped<IUserCompanyRepository, UserCompanyRepo>();
            services.AddScoped<IUserCompanyService, UserCompanyManager>();



            return services;
        }

        //protected override void Load(ContainerBuilder builder)
        //{
        //    builder.RegisterType<CompanyManager>().As<ICompanyService>();
        //    builder.RegisterType<CompanyRepo>().As<ICompanyRepository>();

        //    builder.RegisterType<AccountReconciliationManager>().As<IAccountReconciliationService>();
        //    builder.RegisterType<AccountReconciliatonRepo>().As<IAccountReconciliatonRepository>();

        //    builder.RegisterType<AccountReconciliationDetailManager>().As<IAccountReconciliationDetailService>();
        //    builder.RegisterType<AccountReconciliationDetailRepo>().As<IAccountReconciliationDetailRepository>();


        //    builder.RegisterType<BaBsReconciliationDetailManager>().As<IBaBsReconciliationDetailService>();
        //    builder.RegisterType<BaBsReconciliationDetailRepo>().As<IBaBsReconciliationDetailRepository>();


        //    builder.RegisterType<BaBsReconciliationManager>().As<IBaBsReconciliationService>();
        //    builder.RegisterType<BaBsReconciliationRepo>().As<IBaBsReconciliationRepository>();


        //    //builder.RegisterType<CurrencyAccountManager>().As<ICurrencyAccountService>();
        //    builder.RegisterType<CurrencyAccountRepo>().As<ICurrencyAccountRepository>();

        //    builder.RegisterType<CurrencyManager>().As<ICurrencyService>();
        //    builder.RegisterType<CurrencyRepo>().As<ICurrencyRepository>();


        //    builder.RegisterType<MailParameterManager>().As<IMailParameterService>();
        //    builder.RegisterType<MailParameterRepo>().As<IMailParameterRepository>();

        //    builder.RegisterType<MailManager>().As<IMailService>();
        //    builder.RegisterType<MailRepo>().As<IMailRepository>();

        //    builder.RegisterType<MailTemplateManager>().As<IMailTemplateService>();
        //    builder.RegisterType<MailTemplateRepo>().As<IMailTemplateRepository>();


        //    builder.RegisterType<UserManager>().As<IUserService>();
        //    builder.RegisterType<UserRepo>().As<IUserRepository>();

        //    builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
        //    builder.RegisterType<OperationClaimRepo>().As<IOperationClaimRepository>();

        //    builder.RegisterType<AuthManager>().As<IAuthService>();
        //    builder.RegisterType<JwtHelper>().As<ITokenHelper>();

        //    builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
        //    builder.RegisterType<UserOperationClaimRepo>().As<IUserOperationClaimRepository>();

        //    builder.RegisterType<TermsandCoditionManager>().As<ITermsandConditionService>();
        //    builder.RegisterType<TermsandConditionRepo>().As<ITermsandConditionsRepository>();

        //    builder.RegisterType<UserForgotPasswordManager>().As<IUserForgotPasswordService>();
        //    builder.RegisterType<UserForgotPasswordRepo>().As<IUserForgotPasswordRepository>();

        //    builder.RegisterType<UserRelationshipManager>().As<IUserRelationshipService>();
        //    builder.RegisterType<UserRelationshipRepo>().As<IUserRelationshipRepository>();


        //    var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        //    builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
        //    {
        //        Selector = new AspectInterceptorSelector()
        //    }).SingleInstance();

        //    base.Load(builder);
        //}
    }
}
