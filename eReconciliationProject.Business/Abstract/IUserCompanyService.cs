using eReconciliationProject.Core.Concrete;

namespace eReconciliationProject.Business.Abstract;
public interface IUserCompanyService
{
    void Delete(UserCompany userCompany);
    UserCompany GetByUserIdAndCompanyId(int userId, int companyId);
}
