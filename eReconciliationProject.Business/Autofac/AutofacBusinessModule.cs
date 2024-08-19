using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Concrete;
using eReconciliationProject.Core.Utilities.Interceptors;
using eReconciliationProject.Core.Utilities.Security.JWT;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.DA.Repositories.Concrete;

namespace eReconciliationProject.Business.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyManager>().As<ICompanyService>();
            builder.RegisterType<CompanyRepo>().As<ICompanyRepository>();

            builder.RegisterType<AccountReconciliationManager>().As<IAccountReconciliationService>();
            builder.RegisterType<AccountReconciliatonRepo>().As<IAccountReconciliatonRepository>();

            builder.RegisterType<AccountReconciliationDetailManager>().As<IAccountReconciliationDetailService>();
            builder.RegisterType<AccountReconciliationDetailRepo>().As<IAccountReconciliationDetailRepository>();


            builder.RegisterType<BaBsReconciliationDetailManager>().As<IBaBsReconciliationDetailService>();
            builder.RegisterType<BaBsReconciliationDetailRepo>().As<IBaBsReconciliationDetailRepository>();


            builder.RegisterType<BaBsReconciliationManager>().As<IBaBsReconciliationService>();
            builder.RegisterType<BaBsReconciliationRepo>().As<IBaBsReconciliationRepository>();


            //builder.RegisterType<CurrencyAccountManager>().As<ICurrencyAccountService>();
            builder.RegisterType<CurrencyAccountRepo>().As<ICurrencyAccountRepository>();

            builder.RegisterType<CurrencyManager>().As<ICurrencyService>();
            builder.RegisterType<CurrencyRepo>().As<ICurrencyRepository>();


            builder.RegisterType<MailParameterManager>().As<IMailParameterService>();
            builder.RegisterType<MailParameterRepo>().As<IMailParameterRepository>();

            builder.RegisterType<MailManager>().As<IMailService>();
            builder.RegisterType<MailRepo>().As<IMailRepository>();

            builder.RegisterType<MailTemplateManager>().As<IMailTemplateService>();
            builder.RegisterType<MailTemplateRepo>().As<IMailTemplateRepository>();


            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<UserRepo>().As<IUserRepository>();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<OperationClaimRepo>().As<IOperationClaimRepository>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<UserOperationClaimRepo>().As<IUserOperationClaimRepository>();

            builder.RegisterType<TermsandCoditionManager>().As<ITermsandConditionService>();
            builder.RegisterType<TermsandConditionRepo>().As<ITermsandConditionsRepository>();

            builder.RegisterType<UserForgotPasswordManager>().As<IUserForgotPasswordService>();
            builder.RegisterType<UserForgotPasswordRepo>().As<IUserForgotPasswordRepository>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

            base.Load(builder);
        }
    }
}
