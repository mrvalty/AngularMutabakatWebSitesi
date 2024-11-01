using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Core.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;

namespace eReconciliationProject.Business.Concrete;
public class UserCompanyManager : IUserCompanyService
{
    private readonly IUserCompanyRepository _userCompanyRepository;

    public UserCompanyManager(IUserCompanyRepository userCompanyRepository)
    {
        _userCompanyRepository = userCompanyRepository;
    }

    public void Delete(UserCompany userCompany)
    {
        _userCompanyRepository.Delete(userCompany);
    }

    public UserCompany GetByUserIdAndCompanyId(int userId, int companyId)
    {
        return _userCompanyRepository.Get(x => x.UserId == userId && x.CompanyId == companyId);
    }
}
