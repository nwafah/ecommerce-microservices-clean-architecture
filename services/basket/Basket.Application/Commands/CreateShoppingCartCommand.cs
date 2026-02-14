using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Commands
{
    public record CreateShoppingCartCommand(
        string UserName,
        IReadOnlyCollection<ShoppingCartItemResponse> Items
        ) : IRequest<ShoppingCartResponse>
    {

    }
}
