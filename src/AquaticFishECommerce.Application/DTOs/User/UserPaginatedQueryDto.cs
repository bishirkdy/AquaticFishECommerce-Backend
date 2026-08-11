namespace AquaticFishECommerce.Application.DTOs.User
{
    public class UserPaginatedQueryDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;

        public string? Search { get; set; }
        public string? Status { get; set; }
    }
}
