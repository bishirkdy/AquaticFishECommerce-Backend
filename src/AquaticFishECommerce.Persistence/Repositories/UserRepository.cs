using AquaticFishECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AquaticFishECommerce.Application.Interfaces;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
namespace AquaticFishECommerce.Persistence.Repositories
{
    internal class UserRepository : GenericRepository<User> , IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet
                .AnyAsync(x => x.Email == email);
        }
    }
}
