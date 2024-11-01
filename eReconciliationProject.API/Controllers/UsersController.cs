using eReconciliationProject.Business.Abstract;
using eReconciliationProject.Business.Concrete;
using eReconciliationProject.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliationProject.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly UserManager _userManager;
    private readonly AuthManager _authManager;
    private readonly UserRelationshipManager _userRelationshipManager;

    public UsersController(UserManager userManager, AuthManager authManager, UserRelationshipManager userRelationshipManager, IUserService userService)
    {
        _userManager = userManager;
        _authManager = authManager;
        _userRelationshipManager = userRelationshipManager;
        _userService = userService;
    }

    [HttpGet("getUserList")]
    public IActionResult GetUserList(int companyId)
    {
        var result = _userManager.GetListUserDto(companyId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("getAdminUserList")]
    public IActionResult GetAdminUsersList(int adminUserId)
    {
        var result = _userRelationshipManager.GetListDto(adminUserId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("getAdminCompaniesForUser")]
    public IActionResult GetAdminCompaniesForUser(int adminUserId, int userUserId)
    {
        var result = _userService.GetAdminCompaniesForUser(adminUserId, userUserId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("getUserCompanyList")]
    public IActionResult GetUsersCompanyList(int userId)
    {
        var result = _userRelationshipManager.GetById(userId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("deleteUserCompanyId")]
    public IActionResult UserCompanyDelete(int userId, int companyId)
    {
        var result = _userService.UserCompanyDelete(userId, companyId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("getById")]
    public IActionResult GetByIdUser(int userId)
    {
        var result = _userManager.Get(userId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpPost("update")]
    public IActionResult Update(UserForRegisterToSecondAccountDto userForRegister)
    {
        var findUser = _userManager.GetById(userForRegister.Id);
        if (findUser.Email != userForRegister.Email)
        {
            var userExists = _authManager.UserExists(userForRegister.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);

            }
        }
        var result = _userManager.UpdateResult(userForRegister);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("changeStatus")]
    public IActionResult ChangeStatus(int id)
    {
        var findUser = _userManager.GetById(id);
        if (findUser.IsActive)
        {
            findUser.IsActive = false;
        }
        var result = _authManager.Update(findUser);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("getOperationClaimUser")]
    public IActionResult GetListForUserDto(string value, int companyId)
    {
        var result = _userManager.GetOperationClaimForUserList(value, companyId);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

    [HttpPost("updateOperationClaim")]
    public IActionResult UpdateOperationClaim(OperationClaimForUserListDto operationClaim)
    {
        var result = _userManager.UpdateOperationClaim(operationClaim);
        if (result.Success)
        {
            return Ok(result);
        }
        return BadRequest(result.Message);
    }

}
