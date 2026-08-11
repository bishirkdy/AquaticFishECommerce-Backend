using AquaticFishECommerce.Application.DTOs.Order;
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
            var cartItem = await _dbSet.Where(u => u.UserId == userId).ToListAsync();
            _dbSet.RemoveRange(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartItem?>> GetUserCartAsync(Guid userId)
        {
            return await _dbSet
                .Include(c => c.Product)
                    .ThenInclude(p => p.Images)
                .Where(c =>c.UserId == userId && c.Product.IsActive && c.Product.Stock > 0)
                .ToListAsync();
        }

        public async Task DeleteByProductIdAsync(Guid productId)
        {
            var items = await _context.CartItems
                .Where(x => x.ProductId == productId)
                .ToListAsync();

            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByUserIdAsync(Guid userId)
        {
            var items = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePurchasedItemsAsync(Guid userId, IEnumerable<CreateOrderItemDto> items)
        {
            var productIds = items.Select(x => x.ProductId).ToList();

            var cartItems = await _context.CartItems
                .Where(x =>
                    x.UserId == userId &&
                    productIds.Contains(x.ProductId))
                .ToListAsync();

            foreach (var cartItem in cartItems)
            {
                var orderedItem = items.First(x => x.ProductId == cartItem.ProductId);

                if (orderedItem.Quantity >= cartItem.Quantity)
                {
                    // Purchased entire cart quantity
                    _context.CartItems.Remove(cartItem);
                }
                else
                {
                    // Purchased only part of cart quantity
                    cartItem.Quantity -= orderedItem.Quantity;
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}
