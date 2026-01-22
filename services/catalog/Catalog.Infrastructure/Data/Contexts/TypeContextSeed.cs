using Catalog.Core.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data.Contexts
{
    public static class TypeContextSeed
    {
        public static async Task SeedDataAsync(IMongoCollection<ProductType> typeCollection)
        {
            var hasType = await typeCollection.Find(_ => true).AnyAsync();
            if (hasType)
                return;

            var filePath = Path.Combine("Data", "SeedData", "types.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed File Not Exists {filePath}");
                return;
            }

            var typeData = await File.ReadAllTextAsync(filePath);
            var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

            if (types?.Any() is true)
            {
                await typeCollection.InsertManyAsync(types);
            }
        }
    }
}
