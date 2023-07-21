using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Aspects.Autofac.Transaction;
using eReconciliationProject.Core.Aspects.Caching;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class AccountReconciliationManager : IAccountReconciliationService
    {
        private readonly IAccountReconciliatonRepository _accountReconciliatonRepository;
        private readonly ICurrencyAccountService _currencyAccountService;

        public AccountReconciliationManager(IAccountReconciliatonRepository accountReconciliatonRepository, ICurrencyAccountService currencyAccountService)
        {
            _accountReconciliatonRepository = accountReconciliatonRepository;
            _currencyAccountService = currencyAccountService;
        }
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Add(AccountReconciliaton accountReconciliaton)
        {
            _accountReconciliatonRepository.Add(accountReconciliaton);
            return new SuccessResult(Messages.AddedAccountReconciliation);
        }
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Delete(AccountReconciliaton accountReconciliaton)
        {
            _accountReconciliatonRepository.Delete(accountReconciliaton);
            return new SuccessResult(Messages.DeletedAccountReconciliation);
        }
        [CacheAspect(60)]
        public IDataResult<AccountReconciliaton> GetById(int id)
        {
            return new SuccessDataResult<AccountReconciliaton>(_accountReconciliatonRepository.Get(x => x.Id == id));
        }
        [CacheAspect(60)]
        public IDataResult<List<AccountReconciliaton>> GetList(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliaton>>(_accountReconciliatonRepository.GetList(x => x.CompanyId == companyId));
        }
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Update(AccountReconciliaton accountReconciliaton)
        {
            _accountReconciliatonRepository.Update(accountReconciliaton);
            return new SuccessResult(Messages.UpdatedAccountReconciliation);
        }
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

                        if (code != "Cari Kodu" && code !=null)
                        {
                            DateTime startingDate = reader.GetDateTime(1);
                            DateTime endingDate = reader.GetDateTime(2);
                            double currencyId = reader.GetDouble(3);
                            double debit = reader.GetDouble(4);
                            double credit = reader.GetDouble(5);

                            int currencyAccountId = _currencyAccountService.GetByCode(code, companyId).Data.Id;
                            AccountReconciliaton accountReconciliaton = new AccountReconciliaton()
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId,
                                CurrencyDebit = Convert.ToDecimal(credit),
                                CurrencyCredit = Convert.ToDecimal(debit),
                                CurrencyId = Convert.ToInt32(currencyId),
                                StartingDate = startingDate,
                                EndingDate = endingDate
                            };

                            _accountReconciliatonRepository.Add(accountReconciliaton);
                        }
                    }
                }
                File.Delete(filePath);
            }

            return new SuccessResult(Messages.AddedAccountReconciliation);
        }
    }
}
