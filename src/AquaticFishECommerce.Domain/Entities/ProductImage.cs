using AquaticFishECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
