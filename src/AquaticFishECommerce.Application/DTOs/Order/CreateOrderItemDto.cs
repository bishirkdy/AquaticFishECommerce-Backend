
namespace AquaticFishECommerce.Application.DTOs.Order
{
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
