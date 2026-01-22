using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class CatalogContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<Product> productCollection)
        {
            var hasBrand = await productCollection.Find(_ => true).AnyAsync();
            if (hasBrand)
                return;

            var filePath = Path.Combine("Data", "SeedData", "products.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed File Not Exists {filePath}");
                return;
            }

            var productData = await File.ReadAllTextAsync(filePath);
            var products = JsonSerializer.Deserialize<List<Product>>(productData);

            if (products?.Any() is true)
            {
                await productCollection.InsertManyAsync(products);
            }
        }
    }
}
