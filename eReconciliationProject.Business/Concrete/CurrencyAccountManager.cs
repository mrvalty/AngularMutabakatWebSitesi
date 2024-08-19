using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.BusinessAspect;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Business.ValidationRules.FluentValidation;
using eReconciliationProject.Core.Aspects.Autofac.Transaction;
using eReconciliationProject.Core.Aspects.Autofac.Validation;
using eReconciliationProject.Core.Aspects.Caching;
using eReconciliationProject.Core.Aspects.Performance;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Context;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using ExcelDataReader;
using System.Text;

namespace eReconciliationProject.Business.Concrete
{
    public class CurrencyAccountManager : ICurrencyAccountService
    {
        private readonly ICurrencyAccountRepository _currencyAccountRepository;
        private readonly IAccountReconciliationService _accountReconciliationService;
        private readonly IBaBsReconciliationService _baBsReconciliationService;
        ProjectContext context = new();

        //public CurrencyAccountManager(ICurrencyAccountRepository currencyAccountRepository)
        //{
        //    _currencyAccountRepository = currencyAccountRepository;
        //}

        [PerformanceAspect(3)]
        [SecuredOperation("CurrencyAccountValidator.Add,Admin")]
        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        [ValidationAspect(typeof(CurrencyAccountValidator))]
        public IResult Add(CurrencyAccount currencyAccount)
        {
            _currencyAccountRepository.Add(currencyAccount);
            return new SuccessResult(Messages.AddedCurrencyAccount);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("CurrencyAccountValidator.Delete,Admin")]
        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        public IResult Delete(CurrencyAccount currencyAccount)
        {
            var reconciliaiton = context.AccountReconciliatons.Where(x => x.CurrencyAccountId == currencyAccount.Id).ToList();
            if (reconciliaiton.Count > 0)
            {
                return new ErrorResult(Messages.AccountHaveReconciliation);
            }

            var baBsReconciliation = context.BaBsReconciliations.Where(x => x.CurrencyAccountId == currencyAccount.Id).ToList();
            if (baBsReconciliation.Count > 0)
            {
                return new ErrorResult(Messages.AccountHaveReconciliation);
            }

            //context.CurrencyAccounts.Remove(currencyAccount);
            //_currencyAccountRepository.Delete(currencyAccount);
            var result = context.CurrencyAccounts.Where(x => x.Id == currencyAccount.Id && x.IsActive == true).FirstOrDefault();
            if (result != null)
            {
                result.IsActive = false;
            }
            context.SaveChanges();

            return new SuccessResult(Messages.DeletedCurrencyAccount);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("CurrencyAccountValidator.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<CurrencyAccount> Get(int id)
        {
            return new SuccessDataResult<CurrencyAccount>(_currencyAccountRepository.Get(x => x.Id == id));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("CurrencyAccountValidator.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<CurrencyAccount>> GetList(int companyId)
        {
            var result = context.CurrencyAccounts.Where(p => p.CompanyId == companyId && p.IsActive == true).OrderBy(x => x.Name).ToList();

            return new SuccessDataResult<List<CurrencyAccount>>(result);

            //return new SuccessDataResult<List<CurrencyAccount>>(_currencyAccountRepository.GetList(x => x.CompanyId == companyId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("CurrencyAccountValidator.Update,Admin")]
        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        [ValidationAspect(typeof(CurrencyAccountValidator))]
        public IResult Update(CurrencyAccount currencyAccount)
        {
            _currencyAccountRepository.Update(currencyAccount);
            return new SuccessResult(Messages.UpdatedCurrencyAccount);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("CurrencyAccountValidator.Add,Admin")]
        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        [ValidationAspect(typeof(CurrencyAccountValidator))]
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
                        string name = reader.GetString(1);
                        string address = reader.GetString(2);
                        string taxDepartment = reader.GetString(3);
                        string taxIdNumber = reader.GetString(4);
                        string identityNumber = reader.GetString(5);
                        string email = reader.GetString(6);
                        string authorized = reader.GetString(7);

                        if (code != "Cari Kodu")
                        {
                            CurrencyAccount currencyAccount = new CurrencyAccount()
                            {
                                Name = name,
                                Address = address,
                                TaxDepartment = taxDepartment,
                                TaxIdNumber = taxIdNumber,
                                IdentityNumber = identityNumber,
                                Email = email,
                                Authorized = authorized,
                                AddedAt = DateTime.Now,
                                Code = code,
                                CompanyId = companyId,
                                IsActive = true
                            };

                            _currencyAccountRepository.Add(currencyAccount);
                        }
                    }
                }
            }

            return new SuccessResult(Messages.AddedCurrencyAccount);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("CurrencyAccountValidator.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<CurrencyAccount> GetByCode(string code, int companyId)
        {
            return new SuccessDataResult<CurrencyAccount>(_currencyAccountRepository.Get(x => x.Code == code && x.CompanyId == companyId));
        }
    }
}
