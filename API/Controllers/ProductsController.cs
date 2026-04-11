using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    [HttpGet("[action]")]
    public Task<ActionResult<IEnumerable<Product>>> Get()
    {
        throw new NotImplementedException();
        //        return Ok(new List<Product>());
    }

    [HttpGet("[action]")]
    public Task<ActionResult> GetById()
    {
        return Task.FromResult<ActionResult>(Ok("This is the list of products"));
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        throw new NotImplementedException();
        // return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> Update()
    {
        throw new NotImplementedException();
        // return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult> Delete()
    {
        throw new NotImplementedException();
        // return NoContent();
    }

}
