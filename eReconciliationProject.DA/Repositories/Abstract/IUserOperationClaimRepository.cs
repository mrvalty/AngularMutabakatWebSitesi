using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.DataAccess;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.DA.Repositories.Abstract
{
    public interface IUserOperationClaimRepository : IEntityRepository<UserOperationClaim>
    {
        List<UserOperationClaimDto> GetListDto(int userId, int companyId);
    }
}
