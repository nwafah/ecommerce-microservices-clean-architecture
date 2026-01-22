using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductsByBrandQuery : IRequest<IList<ProductResponseDto>>
    {
        public string BrandName { get; set; }
        public GetProductsByBrandQuery(
            string brandName)
        {
            this.BrandName = brandName;
        }
    }
}
