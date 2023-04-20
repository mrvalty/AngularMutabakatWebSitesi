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
    public class BaBsReconciliationDetailManager : IBaBsReconciliationDetailService
    {
        private readonly IBaBsReconciliationDetailRepository _baBsReconciliationRepository;

        public BaBsReconciliationDetailManager(IBaBsReconciliationDetailRepository baBsReconciliationRepository)
        {
            _baBsReconciliationRepository = baBsReconciliationRepository;
        }
    }
}
