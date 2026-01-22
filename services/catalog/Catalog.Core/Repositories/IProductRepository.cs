using Catalog.Core.Entities;
using Catalog.Core.Specs;

namespace Catalog.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetAllProducts(CatalogSpecParam catalogSpecParam, CancellationToken cancellationToken);
        Task<Product> GetProductById(string id, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetProductsByName(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetProductsByBrand(string name, CancellationToken cancellationToken);
        Task<Product> CreateProduct(Product product, CancellationToken cancellationToken);
        Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken);
        Task<bool> DeleteProduct(string id, CancellationToken cancellationToken);
    }
}
