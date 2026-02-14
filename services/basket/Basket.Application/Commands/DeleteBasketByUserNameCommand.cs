using MediatR;

namespace Basket.Application.Commands
{
    public class DeleteBasketByUserNameCommand : IRequest<Unit>
    {
        public string UserName { get; private set; }
        public DeleteBasketByUserNameCommand(string userName)
        {
            this.UserName = userName;
        }
    }
}
