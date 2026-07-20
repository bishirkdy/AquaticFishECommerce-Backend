using AquaticFishECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> ExistsAsync(Guid id);
    }
}
