using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.Business.Concrete;
public class UserRelationshipManager : IUserRelationshipService
{
    private readonly IUserRelationshipRepository _userRelationshipRepository;

    public UserRelationshipManager(IUserRelationshipRepository userRelationshipRepository)
    {
        _userRelationshipRepository = userRelationshipRepository;
    }

    public void Add(UserRelationship userRelationship)
    {
        _userRelationshipRepository.Add(userRelationship);
    }

    public void Delete(UserRelationship userRelationship)
    {
        _userRelationshipRepository.Delete(userRelationship);
    }

    public IDataResult<UserRelationshipDto> GetById(int userUserId)
    {
        return new SuccessDataResult<UserRelationshipDto>(_userRelationshipRepository.GetById(userUserId));
    }

    public IDataResult<List<UserRelationshipDto>> GetListDto(int adminUserId)
    {
        return new SuccessDataResult<List<UserRelationshipDto>>(_userRelationshipRepository.GetListDto(adminUserId));
    }

    public void Update(UserRelationship userRelationship)
    {
        _userRelationshipRepository.Update(userRelationship);
    }
}
