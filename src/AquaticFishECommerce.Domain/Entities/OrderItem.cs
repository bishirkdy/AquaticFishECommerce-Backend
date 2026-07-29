using AquaticFishECommerce.Domain.Common;
using AquaticFishECommerce.Domain.Enums;


namespace AquaticFishECommerce.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } 
        public decimal CostPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? CancelledAt { get; set; }
    }
}
