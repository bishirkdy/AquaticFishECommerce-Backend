
namespace AquaticFishECommerce.Application.DTOs.Favorite
{
    public class FavoriteResponseDto
    {
        public Guid FavoriteId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
