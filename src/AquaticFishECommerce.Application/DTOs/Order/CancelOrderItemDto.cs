namespace AquaticFishECommerce.Application.DTOs.Order
{
    public class CancelOrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}
