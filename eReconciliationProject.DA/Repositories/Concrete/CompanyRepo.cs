using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.DataAccess.EntityFramework;
using eReconciliationProject.DA.Context;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.DA.Repositories.Concrete
{
    public class CompanyRepo : EfEntityRepositoryBase<Company, ProjectContext>, ICompanyRepository
    {
        public void UserCompanyAdd(int userId, int companyId)
        {
            using (var context = new ProjectContext())
            {
                UserCompany userCompany = new UserCompany()
                {
                    UserId = userId,
                    CompanyId = companyId,
                    AddedAt = DateTime.Now,
                    IsActive = true
                };

                context.UserCompanies.Add(userCompany);
                context.SaveChanges();
            }
        }
    }
}
