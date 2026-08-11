namespace AquaticFishECommerce.Application.DTOs.CartItem
{
    public class CartItemResponseDto
    {
        public Guid CartItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public decimal OriginalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountedPrice { get; set; }

        public int Quantity { get; set; }
        public int AvailableStock { get; set; }
        public decimal TotalPrice { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}
