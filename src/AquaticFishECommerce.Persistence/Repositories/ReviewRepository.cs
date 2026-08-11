using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AquaticFishECommerce.Persistence.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<List<Review>> GetProductReviewsAsync(Guid productId)
        {
            return await _dbSet
                .Include(r => r.User)
                .Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        //public async Task<bool> AlreadyReviewedAsync(Guid userId, Guid productId)
        //{
        //    return await _dbSet.AnyAsync(r =>
        //        r.UserId == userId &&
        //        r.ProductId == productId);
        //}

        public async Task DeleteByProductIdAsync(Guid productId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();

            _context.Reviews.RemoveRange(reviews);
            await _context.SaveChangesAsync();
        }
    }
}
