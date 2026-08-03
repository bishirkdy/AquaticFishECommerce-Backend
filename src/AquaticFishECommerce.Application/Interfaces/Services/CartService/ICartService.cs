using AquaticFishECommerce.Application.DTOs.CartItem;

namespace AquaticFishECommerce.Application.Interfaces.Services.Cart
{
    public interface ICartService
    {
        Task<CartResponseDto> GetCartAsync(Guid userId);
        Task AddToCartAsyn(Guid userId, AddToCartDto dto);
        Task UpdateQuantityAsync(Guid userId, Guid cartItemId, UpdateCartItemDto dto);
        Task RemoveItemAsync(Guid userId, Guid cartItemId);
        Task ClearCartAsync(Guid userId);
    }
}
