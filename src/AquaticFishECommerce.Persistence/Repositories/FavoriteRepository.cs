using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AquaticFishECommerce.Persistence.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite> , IFavoriteRepository
    {
        public FavoriteRepository(AppDbContext context) : base(context) { }

        //Take favorite of user
        public async Task<IEnumerable<Favorite>> GetUserFavoritesAsync(Guid userId)
        {
            return await _dbSet
                .Include(f => f.Product)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        //Ttake of one user and one product that is favorite
        public async Task<Favorite?> GetFavoriteAsync(Guid userId, Guid productId)
        {
            return await _dbSet
                .Include(f => f.Product)
                .FirstOrDefaultAsync(f =>
                    f.UserId == userId &&
                    f.ProductId == productId);
        }

        //Take to clear favorite of one user
        public async Task ClearFavoritesAsync(Guid userId)
        {
            var favorites = await _dbSet
                .Where(f => f.UserId == userId)
                .ToListAsync();

            _dbSet.RemoveRange(favorites);

            await _context.SaveChangesAsync();
        }
    }
}
