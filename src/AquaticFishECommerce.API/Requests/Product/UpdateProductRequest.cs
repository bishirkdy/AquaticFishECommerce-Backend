namespace AquaticFishECommerce.API.Requests.Product
{
    public class UpdateProductRequest
    {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public decimal? Price { get; set; }
            public int? Stock { get; set; }
            public decimal? DiscountPercentage { get; set; }
            public bool? IsActive { get; set; }
            public Guid? CategoryId { get; set; }
            public IFormFile? Image { get; set; }
            public bool IsPrimary { get; set; } = true;
        }
}
