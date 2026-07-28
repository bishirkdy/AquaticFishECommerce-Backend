using AquaticFishECommerce.Domain.Enums;

namespace AquaticFishECommerce.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public Guid AddressId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = [];
    }
}
