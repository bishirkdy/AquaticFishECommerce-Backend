using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.Order
{
    public class CancelOrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}
