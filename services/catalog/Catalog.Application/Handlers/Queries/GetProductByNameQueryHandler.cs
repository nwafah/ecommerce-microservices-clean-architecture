using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetProductsByNameQueryHandler(
            IMapper mapper,
            IProductRepository productRepository
            )
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IList<ProductResponseDto>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductsByName(request.Name, cancellationToken);
            var productResponse = _mapper.Map<IList<ProductResponseDto>>(product);
            return productResponse;
        }
    }
}
