
namespace AquaticFishECommerce.Application.DTOs.Product
{
    public class CreateProductDto 
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public int Stock { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
    }
}
