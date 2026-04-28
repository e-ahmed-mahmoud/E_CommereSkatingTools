using System.Security.Claims;
using API.Helpers;
using Core.Common.Result;
using Core.DTOs.Account;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager) : BaseApiController
{
    private readonly IAccountService _accountService = accountService;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

    [HttpPost("register")]
    public async Task<ActionResult<Result>> RegisterUser(UserRegisterRequest userRegisterRequest)
    {
        var res = await _accountService.RegisterUser(userRegisterRequest);

        return res.IsSuccess ? Ok() : res.ToProblem(StatusCodes.Status400BadRequest);
    }

    [Authorize]
    [HttpGet("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return NoContent();
    }

    [Authorize]
    [HttpGet("user-info")]
    public async Task<ActionResult<Result<UserInfoResponse>>> GetUserInfo()
    {
        var userEmail = User.GetUserEmail();

        if (string.IsNullOrEmpty(userEmail)) return Result.Failure<UserInfoResponse>(UserErrors.NotDefinedUser).ToProblem(401);

        var res = await _accountService.GetUserInfo(userEmail!);
        return res.IsSuccess ? Ok(res.Value) : res.ToProblem(400);
    }

    [Authorize]
    [HttpGet("[action]")]
    public ActionResult<bool> AuthStatus() => User.Identity?.IsAuthenticated ?? false;


    [Authorize]
    [HttpPost("address")]
    public async Task<ActionResult<Result<AddressDto>>> CreateOrUpdateAddress([FromBody] AddressDto address)
    {
        var res = await _accountService.CreateOrUpdateAddress(User.GetUserEmail(), address);

        return res.IsSuccess ? Ok(res.Value) : res.ToProblem(StatusCodes.Status400BadRequest);

    }
}
