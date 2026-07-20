using AquaticFishECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<IEnumerable<Favorite>> GetUserFavoritesAsync(Guid userId);
        Task<Favorite?> GetFavoriteAsync(Guid userId, Guid productId);
        Task ClearFavoritesAsync(Guid userId);

    }
}
