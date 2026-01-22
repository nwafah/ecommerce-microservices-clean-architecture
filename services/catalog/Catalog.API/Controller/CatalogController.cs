using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : BaseApiController
    {
        private readonly IMediator _mediator;

        public CatalogController(
            IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }
        [HttpGet]
        [Route("[action]/{productName}", Name = "GetProductsByProductName")]
        [ProducesResponseType(typeof(IList<ProductResponseDto>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IList<ProductResponseDto>>> GetProductByName(string name)
        public async Task<ActionResult<IList<ProductResponseDto>>> GetProductsByProductName(string productName)
        {
            var query = new GetProductsByNameQuery(productName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponseDto>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IList<ProductResponseDto>>> GetProductByName(string name)
        public async Task<ActionResult<IList<ProductResponseDto>>> GetAllProducts([FromQuery] CatalogSpecParam specs)
        {
            var query = new GetAllProductsQuery(specs);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<BrandResponseDto>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypeResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<TypeResponseDto>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponseDto>> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponseDto>> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
