
namespace AquaticFishECommerce.Application.DTOs.Product
{
    public class ProductQueryDto
    {
        public string? Search { get; set; }
        public string? Category { get; set; }
        public string? Sort { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
