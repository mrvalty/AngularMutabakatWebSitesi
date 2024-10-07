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

    }
}
