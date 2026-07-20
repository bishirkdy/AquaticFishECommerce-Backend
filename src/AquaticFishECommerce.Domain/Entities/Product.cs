using AquaticFishECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<ProductImage> Images { get; set; } = [];
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}
