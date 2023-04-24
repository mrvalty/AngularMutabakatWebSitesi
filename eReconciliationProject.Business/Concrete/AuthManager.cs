using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Constans;
using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Utilities.Hashing;
using eReconciliationProject.Core.Utilities.Results.Abstract;
using eReconciliationProject.Core.Utilities.Results.Concrete;
using eReconciliationProject.Core.Utilities.Security.JWT;
using eReconciliationProject.DA.Repositories.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICompanyService _companyService;
        private readonly IMailService _mailService;
        private readonly IMailParameterService _mailParameterService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICompanyService companyService, IMailService mailService, IMailParameterService mailParameterService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _companyService = companyService;
            _mailService = mailService;
            _mailParameterService = mailParameterService;
        }

        public IResult CompanyExists(Company company)
        {
            var result = _companyService.CompanyExists(company);
            if (!result.Success)
            {
                return new ErrorResult(Messages.CompanyAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user,int companyId)
        {
            var claims = _userService.GetClaims(user, companyId);
            var accessToken = _tokenHelper.CreateToken(user,claims,companyId);
            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<User> Login(UserForLogin userForLogin)
        {
            var userToCkech = _userService.GetByMail(userForLogin.Email);
            if (userToCkech == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLogin.Password, userToCkech.PasswordHash, userToCkech.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCkech, Messages.SuccessfulLogin);
        }

        public IDataResult<UserCompanyDto> Register(UserForRegister userForRegister, string password,Company company)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash, out passwordSalt);
            var user = new User()
            {
                Email = userForRegister.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash=passwordHash,
                PasswordSalt=passwordSalt,
                Name=userForRegister.Name,
            };
            _userService.Add(user);
            _companyService.Add(company);

            _companyService.UserCompanyAdd(user.Id,company.Id);

            UserCompanyDto userCompanyDto = new UserCompanyDto()
            {
                Id=user.Id,
                Name=user.Name,
                Email=user.Email,
                AddedAt=DateTime.Now,
                CompanyId=company.Id,
                IsActive=true,
                MailConfirm=user.MailConfirm,
                MailConfirmDate=user.MailConfirmDate,
                MailConfirmValue=user.MailConfirmValue,
                PasswordHash=user.PasswordHash,
                PasswordSalt=user.PasswordSalt
            };
            var mailparam = _mailParameterService.Get(4);
            SendMailDto sendMailDto = new SendMailDto()
            {
                mailParameter = mailparam.Data,
                tomail = user.Email,
                subject = "Kullanıcı Onay Maili",
                body = "Kullanıcınız sisteme kayıt oldu.Kaydı tamamlamak için lütfen aşağıdaki maile tıklayınız..."
            };

            _mailService.SendMail(sendMailDto);

            return new SuccessDataResult<UserCompanyDto>(userCompanyDto, Messages.UserRegistered);
        }

        public IDataResult<User> RegisterSecondAccount(UserForRegister userForRegister, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User()
            {
                Email = userForRegister.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userForRegister.Name,
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if(_userService.GetByMail(email) !=null)
            {
                return new ErrorResult(Messages.UserAlReadyExists);
            }
            return new SuccessResult();
        }
    }
}
