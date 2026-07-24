using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(p => p.Id == id);
        }

        public async Task<Product?> GetByIdWithImagesAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllWithImagesAsync()
        {
            return await _context.Products
                .Include(p => p.Images)
                .ToListAsync();
        }
    }
}
