using AquaticFishECommerce.Application.DTOs.Order;
using AquaticFishECommerce.Domain.Entities;


namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<CartItem?> GetCartItemAsync(Guid userId, Guid productId);
        Task ClearCartAsync(Guid userId);
        Task<IEnumerable<CartItem?>> GetUserCartAsync(Guid userId);
        Task DeleteByProductIdAsync(Guid productId);
        Task DeleteByUserIdAsync(Guid userId);
        Task RemovePurchasedItemsAsync(Guid userId, IEnumerable<CreateOrderItemDto> orderItems);
    }
}
