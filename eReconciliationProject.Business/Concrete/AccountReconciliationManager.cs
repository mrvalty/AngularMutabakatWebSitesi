using eReconciliationProject.Business.Abstract;
using eReconciliationProject.DA.Repositories.Abstract;
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

        public AccountReconciliationManager(IAccountReconciliatonRepository accountReconciliatonRepository)
        {
            _accountReconciliatonRepository = accountReconciliatonRepository;
        }
    }
}
