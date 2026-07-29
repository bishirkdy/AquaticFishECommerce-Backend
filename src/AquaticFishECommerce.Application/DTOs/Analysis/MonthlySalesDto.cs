using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.Analysis
{
    public class MonthlySalesDto
    {
        public string Month { get; set; } = string.Empty;
        public decimal Sales { get; set; }
        public decimal Profit { get; set; }
    }
}
