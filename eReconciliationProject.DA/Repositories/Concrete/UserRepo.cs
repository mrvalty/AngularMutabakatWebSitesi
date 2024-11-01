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

        public List<OperationClaimForUserListDto> GetOperationClaimForUserList(string value, int companyId)
        {
            using (var context = new ProjectContext())
            {
                var user = context.Users.Where(p => p.MailConfirmValue == value).FirstOrDefault();

                var result = (from operationClaim in context.OperationClaims
                              where operationClaim.Name != "Admin" && !operationClaim.Name.Contains("UserOperationClaim")
                              select new OperationClaimForUserListDto
                              {
                                  Id = operationClaim.Id,
                                  Name = operationClaim.Name,
                                  Description = operationClaim.Description,
                                  Status = (context.UserOperationClaims.Where(p => p.UserId == user.Id && p.OperationClaimId == operationClaim.Id && p.CompanyId == companyId).Count() > 0 ? true : false),
                                  UserName = user.Name,
                                  UserId = user.Id,
                                  CompanyId = companyId
                              }).OrderBy(x => x.Name).ToList();
                return result;
            }
        }

        public List<AdminCompaniesForUserDto> GetAdminCompaniesForUser(int adminUserId, int userUserId)
        {

            using (var context = new ProjectContext())
            {
                var result = (from userCompany in context.UserCompanies.Where(x => x.UserId == adminUserId)
                              join company in context.Companies on userCompany.CompanyId equals company.Id
                              select new AdminCompaniesForUserDto
                              {
                                  Id = company.Id,
                                  Name = company.Name,
                                  AddedAt = company.AddedAt,
                                  Address = company.Address,
                                  IdentityNumber = company.IdentityNumber,
                                  IsActive = company.IsActive,
                                  TaxDepartment = company.TaxDepartment,
                                  TaxIdNumber = company.TaxIdNumber,
                                  IsThere = (context.UserCompanies.Any(x => x.UserId == userUserId && x.CompanyId == company.Id))
                              }).OrderBy(x => x.Name).ToList();
                return result;
            }

            throw new NotImplementedException();
        }
    }
}
