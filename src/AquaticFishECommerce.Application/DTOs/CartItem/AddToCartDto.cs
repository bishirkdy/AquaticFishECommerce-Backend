namespace AquaticFishECommerce.Application.DTOs.CartItem
{
    public class AddToCartDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
