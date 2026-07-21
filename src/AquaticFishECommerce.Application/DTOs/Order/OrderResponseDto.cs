using AquaticFishECommerce.Application.DTOs.Address;
using AquaticFishECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.Order
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderedAt { get; set; }
        public AddressResponseDto ShippingAddress { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = [];
    }
}
