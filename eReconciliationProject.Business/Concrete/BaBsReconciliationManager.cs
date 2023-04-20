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
    public class BaBsReconciliationManager : IBaBsReconciliationService
    {
        private readonly IBaBsReconciliationRepository _baBsReconciliationRepository;

        public BaBsReconciliationManager(IBaBsReconciliationRepository baBsReconciliationRepository)
        {
            _baBsReconciliationRepository = baBsReconciliationRepository;
        }
    }
}
