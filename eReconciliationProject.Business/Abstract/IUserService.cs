using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Entities.Dtos;

namespace eReconciliationProject.Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user, int companyId);
        void Add(User user);
        void Update(User user);
        IResult UpdateResult(UserForRegisterToSecondAccountDto userForRegister);
        User GetByMail(string email);
        User GetById(int id);
        User GetByMailConfirmValue(string value);
        IDataResult<List<UserCompanyForListDto>> GetListUserDto(int companyId);
        IDataResult<User> Get(int id);
        IDataResult<List<OperationClaimForUserListDto>> GetOperationClaimForUserList(string value, int companyId);
        IResult UpdateOperationClaim(OperationClaimForUserListDto operationClaim);

        IResult UserCompanyDelete(int userId, int companyId);
        IDataResult<List<AdminCompaniesForUserDto>> GetAdminCompaniesForUser(int userId, int companyId);

    }
}
