using AquaticFishECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Domain.Entities
{
    public class Review : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
