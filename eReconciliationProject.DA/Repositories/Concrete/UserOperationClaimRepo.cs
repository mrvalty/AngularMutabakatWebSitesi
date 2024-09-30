using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.DataAccess.EntityFramework;
using eReconciliationProject.DA.Context;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.DA.Repositories.Concrete
{
    public class UserOperationClaimRepo : EfEntityRepositoryBase<UserOperationClaim, ProjectContext>, IUserOperationClaimRepository
    {
        public List<UserOperationClaimDto> GetListDto(int userId, int companyId)
        {
            using (var context = new ProjectContext())
            {
                var query = from userOperationClaim in context.UserOperationClaims
                            join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals operationClaim.Id
                            where userOperationClaim.UserId == userId && userOperationClaim.CompanyId == companyId
                            select new UserOperationClaimDto
                            {
                                UserId = userId,
                                Id = operationClaim.Id,
                                CompanyId = companyId,
                                OperationClaimId = operationClaim.Id,
                                OperationClaimDescription = operationClaim.Description,
                                OperationClaimName = operationClaim.Name,
                            };
                return query.ToList();
            }
        }
    }
}
