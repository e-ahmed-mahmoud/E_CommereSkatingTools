using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Common.Result;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data.Services;

public class CartService(IConnectionMultiplexer connectionMultiplexer) : ICartService
{
    private readonly IDatabase _redisDb = connectionMultiplexer.GetDatabase();

    public async Task<Result<ShoppingCart>> GetCartAsync(string key)
    {
        var cart = await _redisDb.StringGetAsync(key);
        if (cart.IsNullOrEmpty)
            return Result.Failure<ShoppingCart>(GenericErrors.OperationError);

        var res = JsonSerializer.Deserialize<ShoppingCart>((byte[])cart!);
        return Result.Success(res!);
    }

    public async Task<Result<ShoppingCart>> SetCartAsync(ShoppingCart cart)
    {
        var cartSerlized = JsonSerializer.Serialize(cart);
        var res = await _redisDb.StringSetAsync(cart.Id, cartSerlized, TimeSpan.FromDays(30));

        if (!res) return Result.Failure<ShoppingCart>(GenericErrors.OperationError);

        var dataRes = await GetCartAsync(cart.Id);
        return Result.Success(dataRes.Value);
    }

    public async Task<Result> DeleteAsync(string key)
    {
        var res = await _redisDb.KeyDeleteAsync(key);
        return res ? Result.Success() : Result.Failure(GenericErrors.OperationError);
    }
}
