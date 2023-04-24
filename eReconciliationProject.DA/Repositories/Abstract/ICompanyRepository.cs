using eReconciliationProject.Core.DataAccess;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.DA.Repositories.Abstract
{
    public interface ICompanyRepository :IEntityRepository<Company>
    {
        void UserCompanyAdd(int userId, int companyId);
    }
}
