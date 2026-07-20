using AquaticFishECommerce.Application.DTOs.Favorite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services
{
    public interface IFavoriteService
    {
        Task<FavoriteListResponseDto> GetFavoritesAsync(Guid userId);
        Task AddFavoriteAsync(Guid userId, AddFavoriteDto dto);
        Task RemoveFavoriteAsync(Guid userId, Guid favoriteId);
        Task ClearFavoritesAsync(Guid userId);
    }
}
