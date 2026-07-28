using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Domain.Enums;


namespace AquaticFishECommerce.Application.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }
        public Guid AddressId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } 
        public List<CreateOrderItemDto> Items { get; set; } = [];
    }
}
