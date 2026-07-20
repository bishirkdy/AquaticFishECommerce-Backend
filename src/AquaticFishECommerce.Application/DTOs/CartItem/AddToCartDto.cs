using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.CartItem
{
    public class AddToCartDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
