using AquaticFishECommerce.Application.DTOs.Favorite;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AquaticFishECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return Guid.Parse(userId);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var userId = GetUserId();

            var favorites = await _favoriteService.GetFavoritesAsync(userId);

            return Ok(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(AddFavoriteDto dto)
        {
            var userId = GetUserId();

            await _favoriteService.AddFavoriteAsync(userId, dto);

            return Ok(new
            {
                Message = "Product added to favorites successfully."
            });
        }

        [HttpDelete("{favoriteId:guid}")]
        public async Task<IActionResult> RemoveFavorite(Guid favoriteId)
        {
            var userId = GetUserId();

            await _favoriteService.RemoveFavoriteAsync(userId, favoriteId);

            return Ok(new
            {
                Message = "Favorite removed successfully."
            });
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearFavorites()
        {
            var userId = GetUserId();

            await _favoriteService.ClearFavoritesAsync(userId);

            return Ok(new
            {
                Message = "Favorites cleared successfully."
            });
        }
    }
}