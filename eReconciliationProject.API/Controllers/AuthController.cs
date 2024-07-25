using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Entities.Concrete;
using eReconciliationProject.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliationProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
            var userExists = _authService.UserExists(userForRegisterToSecond.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);

            }

            var registerResult = _authService.RegisterSecondAccount(userForRegisterToSecond, userForRegisterToSecond.Password, userForRegisterToSecond.CompanyId);
            var result = _authService.CreateAccessToken(registerResult.Data, userForRegisterToSecond.CompanyId);
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
            if(userToLogin.Data.IsActive)
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
            if(user.MailConfirm)
            {
                return BadRequest("Kullanıcı daha önce maili onaylamış.");
            }

            var result= _authService.SendConfirmedEmail(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
