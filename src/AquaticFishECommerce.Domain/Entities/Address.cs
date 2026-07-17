using AquaticFishECommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Domain.Entities
{
    public class Address : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Street { get; set; } = string.Empty;
        public string Post { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;
        public string? Landmark { get; set; }
    }
}
