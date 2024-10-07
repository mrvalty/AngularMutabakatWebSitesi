﻿using eReconciliationProject.Business.Concrete;
using eReconciliationProject.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace eReconciliationProject.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    //private readonly IUserService _userService;
    private readonly UserManager _userManager;
    private readonly AuthManager _authManager;

    public UsersController(UserManager userManager, AuthManager authManager)
    {
        _userManager = userManager;
        _authManager = authManager;
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

}