using AquaticFishECommerce.Domain.Common;
using AquaticFishECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid AddressId { get; set; }
        public Address Address { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? PaymentId { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];
    }
}
