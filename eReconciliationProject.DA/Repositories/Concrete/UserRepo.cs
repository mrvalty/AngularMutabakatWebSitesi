using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.DataAccess.EntityFramework;
using eReconciliationProject.DA.Context;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.DA.Repositories.Concrete
{
    public class UserRepo : EfEntityRepositoryBase<User, ProjectContext>, IUserRepository
    {
        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            using (var context = new ProjectContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.CompanyId == companyId && userOperationClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name,
                             };
                return result.ToList();
            }

            throw new NotImplementedException();
        }

        public List<UserCompanyForListDto> GetUserListDto(int companyId)
        {
            using (var context = new ProjectContext())
            {
                var result = from userCompany in context.UserCompanies.Where(x => x.CompanyId == companyId && x.IsActive == true)
                             join user in context.Users on userCompany.UserId equals user.Id
                             select new UserCompanyForListDto
                             {
                                 Id = userCompany.Id,
                                 UserId = userCompany.UserId,
                                 CompanyId = companyId,
                                 Email = user.Email,
                                 Name = user.Name,
                                 UserAddedAt = user.AddedAt,
                                 UserIsActive = user.IsActive
                             };
                return result.OrderBy(x => x.Name).ToList();
            }
        }
    }
}
