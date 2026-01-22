using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductsByBrandHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProductsByBrandHandler(
            IMapper mapper,
            IProductRepository productRepository
            )
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponseDto>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductsByBrand(request.BrandName, cancellationToken);
            var productResponse = _mapper.Map<IList<ProductResponseDto>>(product);
            return productResponse;
        }
    }
}
