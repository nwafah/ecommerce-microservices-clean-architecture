using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IBrandRepository, ITypeRepository, IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(
            ICatalogContext catalogContext
            )
        {
            _catalogContext = catalogContext;
        }

        public async Task<Product> GetProductById(string id, CancellationToken cancellationToken)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<Pagination<Product>> GetAllProducts(CatalogSpecParam catalogSpecParam, CancellationToken cancellationToken)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(catalogSpecParam.Search))
            {
                filter = filter & builder.Where(p => p.Name.ToLower().Contains(catalogSpecParam.Search.ToLower()));
            }

            if (!string.IsNullOrEmpty(catalogSpecParam.BrandId))
            {
                var brandFilter = builder.Eq(p => p.Brand.Id, catalogSpecParam.BrandId);
                filter &= brandFilter;
            }

            if (!string.IsNullOrEmpty(catalogSpecParam.TypeId))
            {
                var typeFilter = builder.Eq(p => p.Type.Id, catalogSpecParam.TypeId);
                filter &= typeFilter;
            }

            var totalItems = await _catalogContext.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecParam, filter);

            //return await _catalogContext.Products.Find(p => true).ToListAsync(cancellationToken);
            return new Pagination<Product>(
                catalogSpecParam.PageIndex,
                catalogSpecParam.PageSize,
                (int)totalItems,
                data);
        }
        public async Task<IEnumerable<Product>> GetProductsByBrand(string name, CancellationToken cancellationToken)
        {
            return await _catalogContext.Products.Find(p => p.Brand.Name == name).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name, CancellationToken cancellationToken)
        {
            return await _catalogContext.Products.Find(p => p.Name == name).ToListAsync(cancellationToken);
        }

        public async Task<Product> CreateProduct(Product product, CancellationToken cancellationToken)
        {
            await _catalogContext.Products.InsertOneAsync(product, cancellationToken);
            return product;
        }

        public async Task<bool> DeleteProduct(string id, CancellationToken cancellationToken)
        {
            var deleteProduct = await _catalogContext.Products.DeleteOneAsync(p => p.Id == id, cancellationToken);
            return deleteProduct.IsAcknowledged && deleteProduct.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands(CancellationToken cancellationToken)
        {
            return await _catalogContext.Brands.Find(p => true).ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<ProductType>> GetAllTypes(CancellationToken cancellationToken)
        {
            return await _catalogContext.Types.Find(p => true).ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken)
        {
            var updateProduct = await _catalogContext.Products.ReplaceOneAsync(p => p.Id == product.Id, product, cancellationToken: cancellationToken);
            return updateProduct.IsAcknowledged && updateProduct.ModifiedCount > 0;
        }

        private async Task<IReadOnlyList<Product>> DataFilter(
                CatalogSpecParam catalogSpecParam,
                FilterDefinition<Product> filterDefinition)
        {
            var sortDefn = Builders<Product>.Sort.Ascending("Name");
            if (!string.IsNullOrEmpty(catalogSpecParam.Sort))
            {
                switch (catalogSpecParam.Sort)
                {
                    case "priceAsc":
                        sortDefn = Builders<Product>.Sort.Ascending(p => p.Price);
                        break;
                    case "priceDesc":
                        sortDefn = Builders<Product>.Sort.Descending(p => p.Price);
                        break;
                    default:
                        sortDefn = Builders<Product>.Sort.Ascending("Name");
                        break;
                }
            }
            return await _catalogContext
                .Products
                .Find(filterDefinition)
                .Sort(sortDefn)
                .Skip(catalogSpecParam.PageSize * (catalogSpecParam.PageIndex - 1))
                .Limit(catalogSpecParam.PageSize)
                .ToListAsync();
        }
    }
}
