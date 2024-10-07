using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Concrete;
using eReconciliationProject.Core.Utilities.Hashing;
using eReconciliationProject.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserForgotPasswordService _userForgotPasswordService;

        private readonly AuthManager _authManager;

        public AuthController(IAuthService authService, IUserForgotPasswordService userForgotPasswordService, AuthManager authManager)
        {
            _authManager = authManager;
            _authService = authService;
            _userForgotPasswordService = userForgotPasswordService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserAndCompanyRegisterDto userAndCompanyRegister)
        {
            var userExists = _authService.UserExists(userAndCompanyRegister.userForRegister.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);

            }

            var companyExists = _authService.CompanyExists(userAndCompanyRegister.company);
            if (!companyExists.Success)
            {
                return BadRequest(companyExists.Message);

            }

            var registerResult = _authService.Register(userAndCompanyRegister.userForRegister, userAndCompanyRegister.userForRegister.Password, userAndCompanyRegister.company);
            var result = _authService.CreateAccessToken(registerResult.Data, registerResult.Data.CompanyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(registerResult.Message);
        }

        [HttpPost("registerSecondAccount")]
        public IActionResult RegisterSecondAccount(UserForRegisterToSecondAccountDto userForRegisterToSecond)
        {
            //var userExists = _authService.UserExists(userForRegisterToSecond.Email);
            var userExists = _authManager.UserExists(userForRegisterToSecond.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);

            }

            //var registerResult = _authService.RegisterSecondAccount(userForRegisterToSecond, userForRegisterToSecond.Password, userForRegisterToSecond.CompanyId);

            var registerResult = _authManager.RegisterSecondAccount(userForRegisterToSecond, userForRegisterToSecond.Password, userForRegisterToSecond.CompanyId);

            //var result = _authService.CreateAccessToken(registerResult.Data, userForRegisterToSecond.CompanyId);

            var result = _authManager.CreateAccessToken(registerResult.Data, userForRegisterToSecond.CompanyId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(registerResult.Message);
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            var userToLogin = _authService.Login(userForLogin);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            if (userToLogin.Data.IsActive)
            {
                var userCompany = _authService.GetCompany(userToLogin.Data.Id).Data;
                var result = _authService.CreateAccessToken(userToLogin.Data, userCompany.CompanyId);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Kullanıcı pasif durumda.Aktif etmek için yöneticinizle irtibata geçin.");


        }

        [HttpGet("confirmuser")]

        public IActionResult ConfirmUser(string value)
        {
            var user = _authService.GetByMailConfirmValue(value).Data;
            if (user.MailConfirm)
            {
                return BadRequest("Kullanıcı maili daha önceden onaylanmıştır.");
            }
            user.MailConfirm = true;
            user.MailConfirmDate = DateTime.Now;
            var result = _authService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("sendConfirmEmail")]
        public IActionResult SendConfirmEmail(string email)
        {
            var user = _authService.GetByEmail(email).Data;
            if (user == null)
            {
                return BadRequest("Kullanıcı sistemde mevcut değil.");
            }
            if (user.MailConfirm)
            {
                return BadRequest("Kullanıcı daha önce maili onaylamış.");
            }

            var result = _authService.SendConfirmedEmail(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("forgotPassword")]
        public IActionResult UserForgotPassword(string email)
        {
            var user = _authService.GetByEmail(email).Data;
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı.");
            }

            var lists = _userForgotPasswordService.GetListByUserId(user.Id).Data;
            foreach (var list in lists)
            {
                list.IsActive = false;
                _userForgotPasswordService.Update(list);
            }
            var forgotPassword = _userForgotPasswordService.CreateForgotPassword(user).Data;
            var result = _authService.SendForgotPasswordEmail(user, forgotPassword.Value);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);



        }

        [HttpGet("forgotPasswordLinkCheck")]
        public IActionResult ForgotPasswordLinkCheck(string value)
        {
            var result = _userForgotPasswordService.GetForgotPassword(value);
            if (result == null)
            {
                return BadRequest("Tıkladığınız link geçersizdir");
            }
            if (result.IsActive)
            {
                DateTime date1 = DateTime.Now.AddHours(-1);
                DateTime date2 = DateTime.Now;
                if (result.SendDate >= date1 && result.SendDate <= date2)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest("Tıkladığınız link geçersizdir");

                }

            }
            else
            {
                return BadRequest("Tıkladığınız link geçersizdir");
            }
        }

        [HttpPost("changePasswordToForgotPassword")]
        public IActionResult ChangePasswordToForgotPassword(UserForgotPasswordDto passwordDto)
        {
            var forgotPasswordResult = _userForgotPasswordService.GetForgotPassword(passwordDto.Value);
            forgotPasswordResult.IsActive = false;
            _userForgotPasswordService.Update(forgotPasswordResult);

            var userResult = _authService.GetById(forgotPasswordResult.UserId).Data;

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(passwordDto.Password, out passwordHash, out passwordSalt);
            userResult.PasswordHash = passwordHash;
            userResult.PasswordSalt = passwordSalt;
            var result = _authService.Update(userResult);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
