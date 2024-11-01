using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.DataAccess;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.DA.Repositories.Abstract
{
    public interface IUserRepository : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user, int companyId);
        List<UserCompanyForListDto> GetUserListDto(int companyId);
        List<OperationClaimForUserListDto> GetOperationClaimForUserList(string value, int companyId);

        List<AdminCompaniesForUserDto> GetAdminCompaniesForUser(int adminUserId, int userUserId);
    }
}
