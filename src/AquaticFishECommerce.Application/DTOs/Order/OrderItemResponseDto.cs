using AquaticFishECommerce.Domain.Enums;


namespace AquaticFishECommerce.Application.DTOs.Order
{
    public class OrderItemResponseDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? CancelledAt { get; set; }

        public DateTime? ConfirmedAt { get; set; }
        public DateTime? PackedAt { get; set; }
        public DateTime? ShippingAt { get; set; }
        public DateTime? OutForDeliveryAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        public bool refunded { get; set; }

    }
}
