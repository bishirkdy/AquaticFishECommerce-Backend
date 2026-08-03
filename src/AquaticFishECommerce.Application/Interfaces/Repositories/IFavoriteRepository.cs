using AquaticFishECommerce.Domain.Entities;


namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<IEnumerable<Favorite>> GetUserFavoritesAsync(Guid userId);
        Task<Favorite?> GetFavoriteAsync(Guid userId, Guid productId);
        Task ClearFavoritesAsync(Guid userId);
        Task DeleteByProductIdAsync(Guid productId);
        Task DeleteByUserIdAsync(Guid userId);
    }
}
