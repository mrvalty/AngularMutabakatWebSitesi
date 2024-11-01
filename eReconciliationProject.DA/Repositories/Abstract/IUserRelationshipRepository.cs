using eReconciliationProject.Core.DataAccess;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.DA.Repositories.Abstract;
public interface IUserRelationshipRepository : IEntityRepository<UserRelationship>
{
    List<UserRelationshipDto> GetListDto(int adminUserId);
    UserRelationshipDto GetById(int userUserId);
}
