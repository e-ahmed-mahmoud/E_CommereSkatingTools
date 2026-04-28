using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.Account;

public record UserRegisterRequest(string FirstName, string LastName, string Email, string Password);



