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

            var brandsCollectionName = configuration["DatabaseSettings:BrandsCollection"];
            Brands = database.GetCollection<ProductBrand>(brandsCollectionName);
            var typesCollectionName = configuration["DatabaseSettings:TypesCollection"];
            Types = database.GetCollection<ProductType>(typesCollectionName);
            var productsCollectionName = configuration["DatabaseSettings:ProductsCollection"];
            Products = database.GetCollection<Product>(productsCollectionName);

            _ = BrandContextSeed.SeedDataAsync(Brands);
            _ = TypeContextSeed.SeedDataAsync(Types);
            _ = CatalogContextSeed.SeedDataAsync(Products);

        }
    }
}
