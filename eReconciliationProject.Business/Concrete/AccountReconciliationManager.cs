using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.BusinessAspect;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Aspects.Autofac.Transaction;
using eReconciliationProject.Core.Aspects.Caching;
using eReconciliationProject.Core.Aspects.Performance;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using ExcelDataReader;
using System.Text;

namespace eReconciliationProject.Business.Concrete
{
    public class AccountReconciliationManager : IAccountReconciliationService
    {
        private readonly IAccountReconciliatonRepository _accountReconciliatonRepository;
        private readonly ICurrencyAccountService _currencyAccountManager;
        private readonly IMailService _mailService;
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IMailParameterService _mailParameterService;

        public AccountReconciliationManager(IAccountReconciliatonRepository accountReconciliatonRepository, ICurrencyAccountService currencyAccountService, IMailService mailService, IMailTemplateService mailTemplateService, IMailParameterService mailParameterService)
        {
            _accountReconciliatonRepository = accountReconciliatonRepository;
            _currencyAccountManager = currencyAccountService;
            _mailService = mailService;
            _mailTemplateService = mailTemplateService;
            _mailParameterService = mailParameterService;
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Add,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Add(AccountReconciliaton accountReconciliaton)
        {
            string guid = Guid.NewGuid().ToString();
            accountReconciliaton.Guid = guid;
            _accountReconciliatonRepository.Add(accountReconciliaton);
            return new SuccessResult(Messages.AddedAccountReconciliation);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Delete,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Delete(AccountReconciliaton accountReconciliaton)
        {
            _accountReconciliatonRepository.Delete(accountReconciliaton);
            return new SuccessResult(Messages.DeletedAccountReconciliation);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<AccountReconciliaton> GetById(int id)
        {
            return new SuccessDataResult<AccountReconciliaton>(_accountReconciliatonRepository.Get(x => x.Id == id));
        }


        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<AccountReconciliaton>> GetList(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliaton>>(_accountReconciliatonRepository.GetList(x => x.CompanyId == companyId));
        }


        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Update,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Update(AccountReconciliaton accountReconciliaton)
        {
            _accountReconciliatonRepository.Update(accountReconciliaton);
            return new SuccessResult(Messages.UpdatedAccountReconciliation);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.Add,Admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int companyId)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);

                        if (code != "Cari Kodu" && code != null)
                        {
                            DateTime startingDate = reader.GetDateTime(1);
                            DateTime endingDate = reader.GetDateTime(2);
                            double currencyId = reader.GetDouble(3);
                            double debit = reader.GetDouble(4);
                            double credit = reader.GetDouble(5);

                            int currencyAccountId = _currencyAccountManager.GetByCode(code, companyId).Data.Id;
                            string guid = Guid.NewGuid().ToString();
                            AccountReconciliaton accountReconciliaton = new AccountReconciliaton()
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId,
                                CurrencyDebit = Convert.ToDecimal(credit),
                                CurrencyCredit = Convert.ToDecimal(debit),
                                CurrencyId = Convert.ToInt32(currencyId),
                                StartingDate = startingDate,
                                EndingDate = endingDate,
                                Guid = guid
                            };

                            _accountReconciliatonRepository.Add(accountReconciliaton);
                        }
                    }
                }
                File.Delete(filePath);
            }

            return new SuccessResult(Messages.AddedAccountReconciliation);
        }

        [PerformanceAspect(3)]
        public IDataResult<AccountReconciliaton> GetByCode(string code)
        {
            return new SuccessDataResult<AccountReconciliaton>(_accountReconciliatonRepository.Get(x => x.Guid == code));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.SendMail,Admin")]
        public IResult SendReconciliationMail(AccountReconciliationDto accountReconciliatonDto)
        {
            string subject = "Mutabakat Maili";
            string body = $"Gönderici Şirket  : {accountReconciliatonDto.CompanyName} <br/>" +
                          $"Gönderici Vergi Dairesi : {accountReconciliatonDto.CompanyTaxDepartment} <br/>" +
                          $"Gönderici Vergi Numarası : {accountReconciliatonDto.CompanyIdentityNumber} <br/> <hr>" +
                          $"Alıcı Şirket : {accountReconciliatonDto.AccountName} <br/>" +
                          $"Alıcı Vergi Dairesi : {accountReconciliatonDto.AccountTaxDepartment} <br/>" +
                          $"Alıcı Vergi Numarası : {accountReconciliatonDto.AccountTaxIdNumber} - {accountReconciliatonDto.AccountIdentityNumber}  <br/> <hr>" +
                          $"Borç : {accountReconciliatonDto.CurrencyDebit} {accountReconciliatonDto.CurrencyCode} <br/>" +
                          $"Alacak : {accountReconciliatonDto.CurrencyCredit} {accountReconciliatonDto.CurrencyCode} <br/>";
            string link = "https://localhost:7256/api/AccountReconciliations/GetByCode?code=" + accountReconciliatonDto.Guid;
            string linkDescription = "Mutabakatı Cevaplamak için Tıklayın";

            var mailTemplate = _mailTemplateService.GetByTemplateName("Kayıt", 4);
            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", subject);
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);


            var mailparam = _mailParameterService.Get(4);
            Entities.Dtos.SendMailDto sendMailDto = new Entities.Dtos.SendMailDto()
            {
                mailParameter = mailparam.Data,
                tomail = accountReconciliatonDto.AccountEmail,
                subject = subject,
                body = templateBody
            };
            _mailService.SendMail(sendMailDto);

            return new SuccessResult(Messages.MailSendSuccess);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<AccountReconciliationDto>> GetListDto(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliationDto>>(_accountReconciliatonRepository.GetAllDto(companyId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliation.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<AccountReconciliaton>> GetByIdCurrencyAccount(int currencyAccountId)
        {
            return new SuccessDataResult<List<AccountReconciliaton>>(_accountReconciliatonRepository.GetList(x => x.CurrencyAccountId == currencyAccountId));

        }
    }
}
