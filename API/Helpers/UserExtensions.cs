using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Common.Result;

namespace API.Helpers;

public static class UserExtensions
{
    public static string GetUserEmail(this ClaimsPrincipal user)
    {
        if (user.Identity?.IsAuthenticated == false)
            return "";

        var userEmail = user.FindFirstValue(ClaimTypes.Email);
        return userEmail ?? "";
    }
}
