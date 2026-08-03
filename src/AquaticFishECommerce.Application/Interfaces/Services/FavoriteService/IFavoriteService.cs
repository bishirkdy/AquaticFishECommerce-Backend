using AquaticFishECommerce.Application.DTOs.Favorite;

namespace AquaticFishECommerce.Application.Interfaces.Services.Favorite
{
    public interface IFavoriteService
    {
        Task<FavoriteListResponseDto> GetFavoritesAsync(Guid userId);
        Task AddFavoriteAsync(Guid userId, AddFavoriteDto dto);
        Task RemoveFavoriteAsync(Guid userId, Guid favoriteId);
        Task ClearFavoritesAsync(Guid userId);
    }
}
