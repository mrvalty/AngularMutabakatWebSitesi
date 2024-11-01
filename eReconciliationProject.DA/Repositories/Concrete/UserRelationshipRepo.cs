using eReconciliationProject.Core.DataAccess.EntityFramework;
using eReconciliationProject.DA.Context;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.DA.Repositories.Concrete;
public class UserRelationshipRepo : EfEntityRepositoryBase<UserRelationship, ProjectContext>, IUserRelationshipRepository
{
    public UserRelationshipDto GetById(int userUserId)
    {
        using (var context = new ProjectContext())
        {
            var result = from userRelationship in context.UserRelationships.Where(x => x.UserUserId == userUserId)
                         join adminUser in context.Users on userRelationship.AdminUserId equals adminUser.Id
                         join userUser in context.Users on userRelationship.UserUserId equals userUser.Id
                         select new UserRelationshipDto
                         {
                             AdminUserId = adminUser.Id,
                             AdminAddedAt = adminUser.AddedAt,
                             AdminMail = adminUser.Email,
                             AdminIsActive = adminUser.IsActive,
                             AdminUserName = adminUser.Name,
                             Companies = (from userCompany in context.UserCompanies.Where(x => x.UserId == userUser.Id)
                                          join user in context.Users on userCompany.UserId equals user.Id
                                          join company in context.Companies on userCompany.CompanyId equals company.Id
                                          select new Company
                                          {
                                              Id = company.Id,
                                              Name = company.Name,
                                              TaxDepartment = company.TaxDepartment,
                                              TaxIdNumber = company.TaxIdNumber,
                                              IdentityNumber = company.IdentityNumber,
                                              AddedAt = company.AddedAt,
                                              Address = company.Address,
                                              IsActive = company.IsActive,
                                          }).ToList(),
                             Id = userRelationship.Id,
                             UserAddedAt = userUser.AddedAt,
                             UserIsActive = userUser.IsActive,
                             UserMail = userUser.Email,
                             UserUserId = userUser.Id,
                             UserUserName = userUser.Name,
                             UserMailValue = userUser.MailConfirmValue
                         };

            return result.FirstOrDefault();

        }
    }

    public List<UserRelationshipDto> GetListDto(int adminUserId)
    {
        using (var context = new ProjectContext())
        {
            var result = from userRelationship in context.UserRelationships.Where(x => x.AdminUserId == adminUserId)
                         join adminUser in context.Users on userRelationship.AdminUserId equals adminUser.Id
                         join userUser in context.Users on userRelationship.UserUserId equals userUser.Id
                         select new UserRelationshipDto
                         {
                             AdminUserId = adminUser.Id,
                             AdminAddedAt = adminUser.AddedAt,
                             AdminMail = adminUser.Email,
                             AdminIsActive = adminUser.IsActive,
                             AdminUserName = adminUser.Name,
                             Companies = (from userCompany in context.UserCompanies.Where(x => x.UserId == userUser.Id)
                                          join user in context.Users on userCompany.UserId equals user.Id
                                          join company in context.Companies on userCompany.CompanyId equals company.Id
                                          select new Company
                                          {
                                              Id = company.Id,
                                              Name = company.Name,
                                              TaxDepartment = company.TaxDepartment,
                                              TaxIdNumber = company.TaxIdNumber,
                                              IdentityNumber = company.IdentityNumber,
                                              AddedAt = company.AddedAt,
                                              Address = company.Address,
                                              IsActive = company.IsActive,
                                          }).ToList(),
                             Id = userRelationship.Id,
                             UserAddedAt = userUser.AddedAt,
                             UserIsActive = userUser.IsActive,
                             UserMail = userUser.Email,
                             UserUserId = userUser.Id,
                             UserUserName = userUser.Name,
                             UserMailValue = userUser.MailConfirmValue
                         };

            return result.OrderBy(x => x.UserUserName).ToList();

        }
    }
}
