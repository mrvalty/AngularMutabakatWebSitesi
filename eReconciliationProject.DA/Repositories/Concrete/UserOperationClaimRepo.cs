using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.DataAccess.EntityFramework;
using eReconciliationProject.DA.Context;
using eReconciliationProject.DA.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.DA.Repositories.Concrete
{
    public class UserOperationClaimRepo : EfEntityRepositoryBase<UserOperationClaim, ProjectContext>, IUserOperationClaimRepository
    {
    }
}
