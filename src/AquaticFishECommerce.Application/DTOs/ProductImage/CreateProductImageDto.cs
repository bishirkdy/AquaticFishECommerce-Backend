using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.ProductImage
{
    public class CreateProductImageDto 
    {
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public Guid ProductId { get; set; }
    }
}
