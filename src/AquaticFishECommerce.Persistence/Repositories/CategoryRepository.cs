using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        //Method to fetch data by name from database
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.AnyAsync(x => x.Name == name);
        }
        //Method to check if product available by category id
        public async Task<bool> HasProductsAsync(Guid categoryId)
        {
            return await _context.Products.AnyAsync(x => x.CategoryId == categoryId);
        }
    }
}
