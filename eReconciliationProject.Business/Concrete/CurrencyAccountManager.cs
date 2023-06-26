using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Business.ValidationRules.FluentValidation;
using eReconciliationProject.Core.Aspects.Autofac.Validation;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class CurrencyAccountManager : ICurrencyAccountService
    {
        private readonly ICurrencyAccountRepository _currencyAccountRepository;

        public CurrencyAccountManager(ICurrencyAccountRepository currencyAccountRepository)
        {
            _currencyAccountRepository = currencyAccountRepository;
        }
        [ValidationAspect(typeof(CurrencyAccountValidator))]
        public IResult Add(CurrencyAccount currencyAccount)
        {
            _currencyAccountRepository.Add(currencyAccount);
            return new SuccessResult(Messages.AddedCurrencyAccount);
        }

        public IResult Delete(CurrencyAccount currencyAccount)
        {
            _currencyAccountRepository.Delete(currencyAccount);
            return new SuccessResult(Messages.DeletedCurrencyAccount);
        }

        public IDataResult<CurrencyAccount> Get(int id)
        {
           return new SuccessDataResult<CurrencyAccount>(_currencyAccountRepository.Get(x=>x.Id == id));
        }

        public IDataResult<List<CurrencyAccount>> GetList(int companyId)
        {
            return new SuccessDataResult<List<CurrencyAccount>>(_currencyAccountRepository.GetList(x => x.CompanyId == companyId));
        }

        [ValidationAspect(typeof(CurrencyAccountValidator))]
        public IResult Update(CurrencyAccount currencyAccount)
        {
            _currencyAccountRepository.Update(currencyAccount);
            return new SuccessResult(Messages.UpdatedCurrencyAccount);
        }
    }
}
