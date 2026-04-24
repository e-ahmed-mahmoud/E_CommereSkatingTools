using Core.Common.Result;
using Core.DTOs.Product;
using Core.Entities;
using Core.Errors;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> productRepository) : BaseApiController
{
    private readonly IGenericRepository<Product> _productRepository = productRepository;

    [HttpGet]
    public async Task<ActionResult<Result>> Get([FromQuery] ProductSpecParams specParams)
    {
        var specificaiton = new ProductSpecification(specParams);

        var res = await CreatePagination<Product, Product>(_productRepository, specificaiton, specParams.PageNumber, specParams.PageSize);

        return Ok(res.IsSuccess ? res.Value : res.Error);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<ProductRespons>>> GetById([FromRoute] Guid id)
    {
        if (!_productRepository.IsExists(id))
            return Result.Failure<ProductRespons>(ProductErrors.NotDefinedProduct);
        var result = await _productRepository.GetByIdAsync(id);
        return Ok(result);
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
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Product product)
    {
        if (!_productRepository.IsExists(product.Id))
            return NotFound();

        _productRepository.Update(product);
        return await _productRepository.SaveChangesAsync() ? NoContent() : BadRequest();

    }

    [HttpDelete("{id}")]
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
