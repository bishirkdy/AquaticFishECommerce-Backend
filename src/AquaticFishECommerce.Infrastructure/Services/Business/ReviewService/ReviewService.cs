using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Review;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.ReviewService;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;

namespace AquaticFishECommerce.Infrastructure.Services.Business.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ReviewService(
            IReviewRepository reviewRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddReviewAsync(Guid userId, CreateReviewDto dto)
        {
            var productExists = await _productRepository.ExistsAsync(dto.ProductId);
            if (!productExists)
                throw new NotFoundException("Product not found");

            var alreadyReviewed = await _reviewRepository
                .AlreadyReviewedAsync(userId, dto.ProductId);

            if (alreadyReviewed)
                throw new BadRequestException("You already reviewed this product");

            var review = _mapper.Map<Review>(dto);
            review.UserId = userId;
            review.CreatedAt = DateTime.UtcNow;

            await _reviewRepository.AddAsync(review);
        }

        public async Task<List<ReviewResponseDto>> GetProductReviewsAsync(Guid productId)
        {
            var reviews = await _reviewRepository.GetProductReviewsAsync(productId);
            return _mapper.Map<List<ReviewResponseDto>>(reviews);
        }
    }
}

