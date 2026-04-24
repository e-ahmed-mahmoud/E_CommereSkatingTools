using Core.Common.Result;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CartController(ICartService cartService) : BaseApiController
{
    private readonly ICartService _cartService = cartService;

    [HttpGet]
    public async Task<ActionResult<Result<ShoppingCart>>> GetById([FromQuery] string key)
    {
        var res = await _cartService.GetCartAsync(key);
        return res.IsSuccess ? Ok(res.Value) : res.ToProblem(400);
    }

    [HttpPost]
    public async Task<ActionResult<Result<ShoppingCart>>> SetShoppingCart([FromBody] ShoppingCart cart)
    {
        var res = await _cartService.SetCartAsync(cart);

        return res.IsSuccess ? Ok(res.Value) : res.ToProblem(400);
    }
    [HttpPut]
    public async Task<ActionResult<Result<ShoppingCart>>> UpdateShoppingCart([FromRoute] string key, [FromBody] ShoppingCart cart)
    {
        var res = await _cartService.DeleteAsync(key);
        if (res.IsSuccess)
            return await _cartService.SetCartAsync(cart);
        return BadRequest();
    }


    [HttpDelete]
    public async Task<ActionResult<Result>> DeleteShoppingCart([FromQuery] string key)
    {
        var res = await _cartService.DeleteAsync(key);
        return res.IsSuccess ? NoContent() : res.ToProblem(StatusCodes.Status400BadRequest);
    }
}
