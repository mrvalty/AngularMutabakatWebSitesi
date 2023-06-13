using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Security.JWT;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password, Company company);
        IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password, int companyId);
        IDataResult<User> Login(UserForLogin userForLogin);
        IDataResult<User> GetByMailConfirmValue(string value);
        IDataResult<User> GetById(int id);
        IResult UserExists(string email);
        IResult SendConfirmedEmail(User user);
        IResult Update(User user);
        IResult CompanyExists(Company company);
        IDataResult<AccessToken> CreateAccessToken(User user, int companyId);
        IDataResult<UserCompany> GetCompany(int userId);
    }
}
