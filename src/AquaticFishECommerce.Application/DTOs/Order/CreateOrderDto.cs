using AquaticFishECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public Guid AddressId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = [];
    }
}
