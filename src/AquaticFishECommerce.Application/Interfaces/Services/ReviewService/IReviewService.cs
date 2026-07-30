using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Review;


namespace AquaticFishECommerce.Application.Interfaces.Services.ReviewService
{
    public interface IReviewService
    {
        Task AddReviewAsync(Guid userId, CreateReviewDto dto);
        Task<List<ReviewResponseDto>> GetProductReviewsAsync(Guid productId);
    }
}
