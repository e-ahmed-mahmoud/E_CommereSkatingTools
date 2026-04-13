using System.Text.Json;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DataSeed;

public class DataSeed
{
    public async static Task SeedingData(StoreDbContext storeDbContext)
    {
        if (await storeDbContext.Products.AnyAsync())
            return;

        var cureentpath = Directory.GetCurrentDirectory();
        var data = await File.ReadAllTextAsync("../Infrastructure/Data/DataSeed/products.json");
        if (string.IsNullOrEmpty(data))
            return;
        var products = JsonSerializer.Deserialize<List<Product>>(data);

        await storeDbContext.Products.AddRangeAsync(products!);
        await storeDbContext.SaveChangesAsync();
    }

}
