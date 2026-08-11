using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AquaticFishECommerce.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address> , IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Address>> GetUserAddressesAsync(Guid userId)
        {
            return await _dbSet
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<Address?> GetLastUsedAddressAsync(Guid userId)
        {
            return await _context.Addresses
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.UpdatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<Address?> FindExistingAddressAsync(Guid userId, string email , string street,string post, string district, string state, string pincode, string? landmark)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a =>
                    a.UserId == userId &&
                    a.Email == email &&
                    a.Street == street &&
                    a.Post == post &&
                    a.District == district &&
                    a.State == state &&
                    a.Pincode == pincode &&
                    a.Landmark == landmark);
        }

    }
}
