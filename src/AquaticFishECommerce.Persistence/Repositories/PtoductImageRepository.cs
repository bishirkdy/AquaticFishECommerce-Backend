using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Persistence.Repositories
{
    public class PtoductImageRepository : GenericRepository<ProductImage> , IProductImageRepository
    {
        public PtoductImageRepository(AppDbContext context) : base(context){}

        public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(Guid productId)
        {
            return await _dbSet
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductImage?> GetPrimaryImageAsync(Guid productId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x =>
                    x.ProductId == productId &&
                    x.IsPrimary);
        }
    }
}
