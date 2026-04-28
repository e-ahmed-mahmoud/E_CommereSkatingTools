using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Account;

public record UserInfoResponse(string FirstName, string LastName, string Email, AddressDto? Address);
