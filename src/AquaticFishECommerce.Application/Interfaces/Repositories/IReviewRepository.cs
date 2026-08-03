

using AquaticFishECommerce.Domain.Entities;

namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<List<Review>> GetProductReviewsAsync(Guid productId);
        Task<bool> AlreadyReviewedAsync(Guid userId, Guid productId);
        Task DeleteByProductIdAsync(Guid productId);
    }
}
