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
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class AccountReconciliationDetailManager : IAccountReconciliationDetailService
    {
        private readonly IAccountReconciliationDetailRepository _accountReconciliationDetailRepository;
        private readonly ICurrencyAccountService _currencyAccountManager;


        public AccountReconciliationDetailManager(IAccountReconciliationDetailRepository accountReconciliationDetailRepository, ICurrencyAccountService currencyAccountService)
        {
            _accountReconciliationDetailRepository = accountReconciliationDetailRepository;
            _currencyAccountManager = currencyAccountService;
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliationDetail.Add,Admin")]
        [CacheRemoveAspect("AccountReconciliationDetail.Get")]
        public IResult Add(AccountReconciliationDetail accountReconciliationDetail)
        {
            _accountReconciliationDetailRepository.Add(accountReconciliationDetail);
            return new SuccessResult(Messages.AddedAccountReconciliationDetail);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliationDetail.Add,Admin")]
        [CacheRemoveAspect("AccountReconciliationDetail.Get")]
        [TransactionScopeAspect]
        public IResult AddToExcel(string filePath, int accountReconciliationId)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string description = reader.GetString(1);

                        if (description != "Açıklama" && description != null)
                        {
                            DateTime date = reader.GetDateTime(0);
                            double currencyId = reader.GetDouble(2);
                            double debit = reader.GetDouble(3);
                            double credit = reader.GetDouble(4);

                            AccountReconciliationDetail accountReconciliationDetail = new AccountReconciliationDetail()
                            {
                                AccountReconciliationId = accountReconciliationId,
                                Description = description,
                                Date = date,
                                CurrencyDebit = Convert.ToDecimal(credit),
                                CurrencyCredit = Convert.ToDecimal(debit),
                                CurrencyId = Convert.ToInt32(currencyId),
                            };

                            _accountReconciliationDetailRepository.Add(accountReconciliationDetail);
                        }
                    }
                }
                File.Delete(filePath);
            }

            return new SuccessResult(Messages.AddedAccountReconciliation);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliationDetail.Delete,Admin")]
        [CacheRemoveAspect("AccountReconciliationDetail.Get")]        
        public IResult Delete(AccountReconciliationDetail accountReconciliationDetail)
        {
            _accountReconciliationDetailRepository.Delete(accountReconciliationDetail);
            return new SuccessResult(Messages.DeletedAccountReconciliationDetail);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliationDetail.Get,Admin")]
        [CacheAspect(60)]
        public IDataResult<AccountReconciliationDetail> GetById(int id)
        {
            return new SuccessDataResult<AccountReconciliationDetail>(_accountReconciliationDetailRepository.Get(x => x.Id == id));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliationDetail.GetList,Admin")]
        [CacheAspect(60)]
        public IDataResult<List<AccountReconciliationDetail>> GetList(int accountReconciliationId)
        {
            return new SuccessDataResult<List<AccountReconciliationDetail>>(_accountReconciliationDetailRepository.GetList(x => x.AccountReconciliationId == accountReconciliationId));
        }

        [PerformanceAspect(3)]
        [SecuredOperation("AccountReconciliationDetail.Update,Admin")]
        [CacheRemoveAspect("AccountReconciliationDetail.Get")]
        public IResult Update(AccountReconciliationDetail accountReconciliationDetail)
        {
            _accountReconciliationDetailRepository.Update(accountReconciliationDetail);
            return new SuccessResult(Messages.UpdatedAccountReconciliationDetail);
        }
    }
}
