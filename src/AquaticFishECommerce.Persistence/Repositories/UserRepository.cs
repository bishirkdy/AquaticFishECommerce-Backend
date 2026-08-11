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

        public async Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int page, int pageSize, string? search, string? status)
        {
            var query = _context.Users
                .Where(u => u.Role != UserRole.Admin)
                .AsNoTracking()
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(u =>
                    u.Name.Contains(search) ||
                    u.Email.Contains(search) ||
                    u.Id.ToString().Contains(search));
            }

            // Status
            if (status == "blocked")
            {
                query = query.Where(u => u.IsBlocked);
            }
            else if (status == "active")
            {
                query = query.Where(u => !u.IsBlocked);
            }

            var totalCount = await query.CountAsync();

            var users = await query
                .OrderBy(u => u.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }
    }
}
