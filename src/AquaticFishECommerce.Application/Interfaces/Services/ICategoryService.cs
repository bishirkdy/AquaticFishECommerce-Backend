using AquaticFishECommerce.Application.DTOs.Category;


namespace AquaticFishECommerce.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto> GetByIdAsync(Guid id);
        Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(Guid id, UpdateCategoryDto dto);
        Task DeleteAsync(Guid id);
    }
}
