using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Result;
using Core.Entities;

namespace Core.Interfaces;

public interface ICartService
{

    Task<Result<ShoppingCart>> GetCartAsync(string key);
    Task<Result<ShoppingCart>> SetCartAsync(ShoppingCart cart);
    Task<Result> DeleteAsync(string key);
}
