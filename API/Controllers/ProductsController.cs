using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> productRepository) : ControllerBase
{
    private readonly IGenericRepository<Product> _productRepository = productRepository;

    [HttpGet("[action]")]
    public async Task<ActionResult<IReadOnlyList<Product>>> Get(string? brand, string? type, string? sort)
    {
        var specificaiton = new ProductSpecification(brand, type, sort);

        return Ok(await _productRepository.GetAllAsync(specificaiton));
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult> GetById([FromRoute] Guid id)
    {
        if (!_productRepository.IsExists(id))
            return NotFound();

        return Ok(await _productRepository.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] Product product)
    {
        _productRepository.Add(product);

        return await _productRepository.SaveChangesAsync() ?
            CreatedAtAction("GetById", new { id = product.Id }, product)
            : BadRequest();
        // return Ok();
    }
    [HttpPut("[action]")]
    public async Task<IActionResult> Update([FromBody] Product product)
    {
        if (!_productRepository.IsExists(product.Id))
            return NotFound();

        _productRepository.Update(product);
        return await _productRepository.SaveChangesAsync() ? NoContent() : BadRequest();

    }

    [HttpDelete("[action]/id")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        _productRepository.Delete(product);

        return await _productRepository.SaveChangesAsync() ? NoContent() : BadRequest();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<string>>> GetBrands()
    {
        var brandSpecification = new BrandListSpecification();
        return Ok(await _productRepository.GetAllAsync(brandSpecification));
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<string>>> GetTypes()
    {
        var typeSepcificaiton = new TypeListSpecification();
        return Ok(await _productRepository.GetAllAsync(typeSepcificaiton));
    }
}
