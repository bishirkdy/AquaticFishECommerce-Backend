namespace AquaticFishECommerce.Application.DTOs.Product
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new();
    }
}
