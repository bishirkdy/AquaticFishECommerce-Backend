using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.Category
{
    public interface ICategoryDto
    {
         string Name { get; }
         string? Description { get;  }
         string? ImageUrl { get;  }
    }
}
