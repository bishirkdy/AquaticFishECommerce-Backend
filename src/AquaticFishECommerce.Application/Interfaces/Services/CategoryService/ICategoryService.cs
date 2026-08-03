using AquaticFishECommerce.Application.DTOs.Category;


namespace AquaticFishECommerce.Application.Interfaces.Services.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto> GetByIdAsync(Guid id);
    }
}
