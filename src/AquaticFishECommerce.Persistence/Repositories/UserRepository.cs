using AquaticFishECommerce.Application.DTOs.User;
using AquaticFishECommerce.Application.Interfaces;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Domain.Enums;
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
            return await _dbSet.AnyAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllAsyncUser()
        {
            return await _dbSet.Where(u => u.Role != UserRole.Admin)
                .ToListAsync();
        }

        public async Task<User?> GetByRefreshTokenHashAsync(string refreshTokenHash)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.RefreshTokenHash == refreshTokenHash);
        }
    }
}
