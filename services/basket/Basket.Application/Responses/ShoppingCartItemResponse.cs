namespace Basket.Application.Responses
{
    public class ShoppingCartItemResponse
    {
        // === there will be validation to check Quantity <= 0 in application layer or Domain Methods
        public int Quantity { get; set; }
        // === there will be validation to check UnitPrice < 0 in application layer or Domain Methods
        public decimal UnitPrice { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ImageFile { get; set; } = string.Empty;
    }
}
