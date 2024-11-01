using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.Business.Abstract;
public interface IUserRelationshipService
{
    void Add(UserRelationship userRelationship);
    void Update(UserRelationship userRelationship);
    void Delete(UserRelationship userRelationship);
    IDataResult<List<UserRelationshipDto>> GetListDto(int adminUserId);
    IDataResult<UserRelationshipDto> GetById(int userUserId);
}
