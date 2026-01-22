using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data.Contexts
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<ProductBrand> Brands { get; }

        public IMongoCollection<ProductType> Types { get; }

        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Brands = database.GetCollection<ProductBrand>("DatabaseSettings:BrandsCollection");
            Types = database.GetCollection<ProductType>("DatabaseSettings:TypesCollection");
            Products = database.GetCollection<Product>("DatabaseSettings:ProductsCollection");

            _ = BrandContextSeed.SeedDataAsync(Brands);
            _ = TypeContextSeed.SeedDataAsync(Types);
            _ = CatalogContextSeed.SeedDataAsync(Products);

        }
    }
}
