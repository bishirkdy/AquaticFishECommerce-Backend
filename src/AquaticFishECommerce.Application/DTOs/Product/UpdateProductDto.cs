namespace AquaticFishECommerce.Application.DTOs.Product
{
    public class UpdateProductDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CategoryId { get; set; }
    }
}