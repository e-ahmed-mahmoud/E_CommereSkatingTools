using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ProductRepository(StoreDbContext storeDbContext) : IProductRepository
{
    private readonly StoreDbContext _storeDbContext = storeDbContext;

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = _storeDbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(brand))
            query = query.Where(p => p.Brand.Contains(brand));

        if (!string.IsNullOrEmpty(type))
            query = query.Where(p => p.Type.Contains(type));

        query = sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };

        return await query.AsNoTracking().ToListAsync();
    }
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _storeDbContext.Products.FindAsync(id);
    }

    public void AddProduct(Product product)
    {
        _storeDbContext.Add(product);
    }
    public void UpdateProduct(Product product)
    {
        _storeDbContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        _storeDbContext.Products.Remove(product);
    }

    public async Task<bool> IsProductExist(Guid id)
    {
        return await _storeDbContext.Products.AnyAsync(p => p.Id == id);

    }

    public async Task<bool> SaveChangesAsnc()
    {
        return await _storeDbContext.SaveChangesAsync() > 0;
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await _storeDbContext.Products.Where(p => !string.IsNullOrEmpty(p.Brand)).AsNoTracking().Select(p => p.Brand).Distinct().ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await _storeDbContext.Products.Where(p => !string.IsNullOrEmpty(p.Brand)).AsNoTracking().Select(p => p.Type).Distinct().ToListAsync();
    }
}
