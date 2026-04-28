using Core.Common.Result;
using Core.DTOs.Account;
using Microsoft.AspNetCore.Identity;

namespace Core.Interfaces;

public interface IAccountService
{
    Task<Result<IdentityResult>> RegisterUser(UserRegisterRequest userRegisterRequest);
    Task<Result<UserInfoResponse>> GetUserInfo(string email);

    Task<Result<AddressDto>> CreateOrUpdateAddress(string email, AddressDto address);

}
