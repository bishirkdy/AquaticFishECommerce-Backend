using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.CartItem
{
    public class CartResponseDto
    {
        public Guid UserId { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = [];
        public decimal GrandTotal { get; set; }
        public int TotalItems { get; set; }
    }
}
