using Basket.Core.Entities;

namespace Basket.Application.Responses
{
    public class ShoppingCartResponse
    {
        public string UserName { get; set; } = string.Empty;
        public List<ShoppingCartItem> Items { get; private set; } = new();

        public ShoppingCartResponse() { }
        public ShoppingCartResponse(string userName) => UserName = userName;

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.UnitPrice * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
