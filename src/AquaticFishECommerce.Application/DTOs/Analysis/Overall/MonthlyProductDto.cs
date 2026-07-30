using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.Analysis.Overall
{
    public class MonthlyProductDto
    {
        public string Month { get; set; } = string.Empty;
        public int ProductCount { get; set; }
    }
}
