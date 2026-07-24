using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.Product
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public List<string> ImageUrls { get; set; } = new();

    }
}
