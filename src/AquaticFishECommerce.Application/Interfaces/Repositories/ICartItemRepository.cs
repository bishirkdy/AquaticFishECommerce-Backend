using AquaticFishECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<CartItem?> GetCartItemAsync(Guid userId, Guid productId);
        Task ClearCartAsync(Guid userId);
        Task<IEnumerable<CartItem?>> GetUserCartAsync(Guid userId);
    }
}
