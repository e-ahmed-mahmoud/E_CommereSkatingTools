using Core.Common.Result;
using Core.DTOs.Account;
using Core.Entities;
using Core.Interfaces;
using Core.ValueObjects;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AccountService(UserManager<ApplicationUser> userManager) : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result<IdentityResult>> RegisterUser(UserRegisterRequest userRegisterRequest)
    {
        var user = new ApplicationUser
        {
            FirstName = userRegisterRequest.FirstName,
            LastName = userRegisterRequest.LastName,
            UserName = userRegisterRequest.Email,
            Email = userRegisterRequest.Email,
        };
        var res = await _userManager.CreateAsync(user, userRegisterRequest.Password);

        if (!res.Succeeded)
            return Result.Failure<IdentityResult>(new(res.Errors.First().Code, res.Errors.First().Description));

        return Result.Success(res);
    }

    public async Task<Result<UserInfoResponse>> GetUserInfo(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            return Result.Failure<UserInfoResponse>(UserErrors.NotDefinedUser);

        return Result.Success(user.Adapt<UserInfoResponse>());
    }

    public async Task<Result<AddressDto>> CreateOrUpdateAddress(string email, AddressDto address)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null) return Result.Failure<AddressDto>(UserErrors.NotDefinedUser);

        user.Address = address.Adapt<Address>();

        var res = await _userManager.UpdateAsync(user);

        return res.Succeeded
            ? Result.Success(address)
            : Result.Failure<AddressDto>(new Error(res.Errors.First().Code, res.Errors.First().Description));

    }
}
