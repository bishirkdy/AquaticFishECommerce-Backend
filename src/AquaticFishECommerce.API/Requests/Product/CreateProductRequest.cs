namespace AquaticFishECommerce.API.Requests.Product
{
    public class CreateProductRequest
    {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public decimal DiscountPercentage { get; set; }
            public bool IsActive { get; set; } = true;
            public Guid CategoryId { get; set; }
            public bool IsPrimary { get; set; } = true;
            public IFormFile? Image { get; set; }
        }
}
