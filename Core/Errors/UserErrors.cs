using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Result;

namespace Core.Entities;

public class UserErrors
{
    public static Error NotDefinedUser => new("NotDefined", "User not defined");

}
