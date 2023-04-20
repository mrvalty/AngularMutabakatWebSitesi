using eReconciliationProject.Business.Abstract;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class CurrencyManager : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyManager(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
    }
}
