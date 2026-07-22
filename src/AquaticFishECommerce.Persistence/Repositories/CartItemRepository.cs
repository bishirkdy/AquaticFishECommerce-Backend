using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AquaticFishECommerce.Persistence.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem> , ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<CartItem>> GetByIdAsyn(Guid userId)
        {
            return await _dbSet.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();
        }

        public async Task<CartItem?> GetCartItemAsync(Guid userId , Guid productId)
        {
            return await _dbSet.Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);
        }

        public async Task ClearCartAsync(Guid userId)
        {
            var cartItem = await _dbSet.Where(u => u.Id == userId).ToListAsync();
            _dbSet.RemoveRange(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
