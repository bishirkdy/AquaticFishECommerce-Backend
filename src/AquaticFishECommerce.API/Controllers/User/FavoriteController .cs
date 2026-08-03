using AquaticFishECommerce.Application.Common.Responses;
using AquaticFishECommerce.Application.DTOs.Favorite;
using AquaticFishECommerce.Application.Interfaces.Services.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaticFishECommerce.API.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }


        //Get favorite of user
        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var favorites = await _favoriteService.GetFavoritesAsync(UserId);

            return Ok(new ApiResponse<FavoriteListResponseDto>
            {
                Success = true,
                Message = "Fetched All Favorite Successfully",
                Data = favorites
                
            });
        }

        //Add to favorite method
        [HttpPost]
        public async Task<IActionResult> AddFavorite(AddFavoriteDto dto)
        {

            await _favoriteService.AddFavoriteAsync(UserId, dto);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product added to favorites successfully."
            });
        }

        //Delete favorite by favorite uniqe id
        [HttpDelete("{productId:guid}")]
        public async Task<IActionResult> RemoveFavorite(Guid productId)
        {
            await _favoriteService.RemoveFavoriteAsync(UserId, productId);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Favorite removed successfully."
            });
        }

        //Clear favorite of a user
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearFavorites()
        {

            await _favoriteService.ClearFavoritesAsync(UserId);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Favorites cleared successfully."
            });
        }
    }
}