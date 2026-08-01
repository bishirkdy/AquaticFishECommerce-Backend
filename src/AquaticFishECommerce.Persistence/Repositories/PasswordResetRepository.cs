using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AquaticFishECommerce.Persistence.Repositories
{
    public class PasswordResetRepository : GenericRepository<PasswordResetToken>, IPasswordResetRepository
    {
        public PasswordResetRepository(AppDbContext context) : base(context) {}

        public async Task<PasswordResetToken?> GetByTokenAsync(string token)
        {
            return await _dbSet
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token);
        }
    }
}
