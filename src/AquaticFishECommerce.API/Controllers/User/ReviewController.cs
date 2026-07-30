using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Review;
using AquaticFishECommerce.Application.Interfaces.Services.ReviewService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    
    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] CreateReviewDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
        {
            return Unauthorized(new ApiResponse<string>
            {
                Success = false,
                Message = "User not authenticated."
            });
        }

        var userId = Guid.Parse(userIdClaim.Value);
        await _reviewService.AddReviewAsync(userId, dto);
        return Ok(new ApiResponse<string>
        {
            Success = true,
            Message = "Review added successfully"
        });
    }

    [AllowAnonymous]
    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetReviews(Guid productId)
    {
        var reviews = await _reviewService.GetProductReviewsAsync(productId);

        return Ok(new ApiResponse<List<ReviewResponseDto>>
        {
            Success = true,
            Message = "Reviews fetched successfully",
            Data = reviews
        });
    }
}