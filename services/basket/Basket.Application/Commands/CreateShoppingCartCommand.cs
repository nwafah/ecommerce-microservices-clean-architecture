using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public record CreateShoppingCartCommand(
        string UserName,
        IReadOnlyCollection<ShoppingCartItem> Items
        ) : IRequest<ShoppingCartResponse>
    {

    }
}
