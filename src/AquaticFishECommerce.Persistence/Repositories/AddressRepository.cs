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

    }
}
