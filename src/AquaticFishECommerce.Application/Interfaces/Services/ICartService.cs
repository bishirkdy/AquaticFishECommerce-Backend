using AquaticFishECommerce.Application.DTOs.CartItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services
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
