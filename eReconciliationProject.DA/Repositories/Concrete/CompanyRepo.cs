using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.DataAccess.EntityFramework;
using eReconciliationProject.DA.Context;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;

namespace eReconciliationProject.DA.Repositories.Concrete
{
    public class CompanyRepo : EfEntityRepositoryBase<Company, ProjectContext>, ICompanyRepository
    {
        public UserCompany GetCompany(int userId)
        {
            using (var context = new ProjectContext())
            {
                var result = context.UserCompanies.Where(c => c.UserId == userId).FirstOrDefault();
                return result;
            }
        }

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

        public List<Company> GetListByUserId(int userId)
        {
            using (var context = new ProjectContext())
            {
                var result = (from userCompany in context.UserCompanies.Where(x => x.UserId == userId)
                              join company in context.Companies on userCompany.CompanyId equals company.Id
                              select new Company()
                              {
                                  Id = company.Id,
                                  AddedAt = company.AddedAt,
                                  Address = company.Address,
                                  IdentityNumber = company.IdentityNumber,
                                  IsActive = company.IsActive,
                                  Name = company.Name,
                                  TaxDepartment = company.TaxDepartment,
                                  TaxIdNumber = company.TaxIdNumber
                              }).OrderBy(x => x.Name).ToList();

                return result;
            }
        }
    }
}
