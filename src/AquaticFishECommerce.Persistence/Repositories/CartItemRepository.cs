using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AquaticFishECommerce.Persistence.Repositories
{
    public class CartItemRepository : GenericRepository<CartItem> , ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context) {}

        //Take user cart items using userid with product
        public async Task<IEnumerable<CartItem>> GetByIdAsyn(Guid userId)
        {
            return await _dbSet.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();
        }

        //Take user cart one item using userid with productid
        public async Task<CartItem?> GetCartItemAsync(Guid userId , Guid productId)
        {
            return await _dbSet.Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);
        }

        //Clear all cart items of user
        public async Task ClearCartAsync(Guid userId)
        {
            var cartItem = await _dbSet.Where(u => u.Id == userId).ToListAsync();
            _dbSet.RemoveRange(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem?>> GetUserCartAsync(Guid userId)
        {
            return await _dbSet
                .Include(c => c.Product)
                    .ThenInclude(p => p.Images)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
