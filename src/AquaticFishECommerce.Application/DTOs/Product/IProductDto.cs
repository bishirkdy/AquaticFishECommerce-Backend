using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.DTOs.Product
{
    public interface IProductDto
    {
        string Name { get; }
        string Description { get; }
        decimal Price { get; }
        int Stock { get; }
        decimal DiscountPercentage { get; }
        Guid CategoryId { get; }
    }
}
