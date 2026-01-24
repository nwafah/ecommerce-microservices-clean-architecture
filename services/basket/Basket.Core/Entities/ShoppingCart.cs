namespace Basket.Core.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = string.Empty;
        public List<ShoppingCartItem> Items { get; private set; } = new();

        public ShoppingCart() { }
        public ShoppingCart(string userName) => UserName = userName;

        public void AddItem(string productId, string productName, decimal unitPrice, int quantity, string imageUrl)
        {
            Items.Add(new ShoppingCartItem
            {
                ProductId = productId,
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity,
                ImageFile = imageUrl
            }
                );
        }

        public void RemoveItem(string productId)
        {
            var product = Items.Find(x => x.ProductId == productId);
            if (product != null)
            {
                Items.Remove(product);
            }
        }
    }
}
