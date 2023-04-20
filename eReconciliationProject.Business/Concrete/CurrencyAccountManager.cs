using eReconciliationProject.Business.Abstract;
using eReconciliationProject.DA.Repositories.Abstract;
using System;
using System.Collections.Generic;
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
    }
}
