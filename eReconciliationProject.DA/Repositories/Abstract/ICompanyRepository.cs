using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.DataAccess;
using eReconciliationProject.Entities.Concrete;

namespace eReconciliationProject.DA.Repositories.Abstract
{
    public interface ICompanyRepository : IEntityRepository<Company>
    {
        void UserCompanyAdd(int userId, int companyId);
        UserCompany GetCompany(int userId);

        List<Company> GetListByUserId(int userId);
    }
}
