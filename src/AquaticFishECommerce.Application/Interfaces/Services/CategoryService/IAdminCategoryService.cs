

using AquaticFishECommerce.Application.DTOs.Category;

namespace AquaticFishECommerce.Application.Interfaces.Services.CategoryService
{
    public interface IAdminCategoryService
    {
        Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(Guid id, UpdateCategoryDto dto);
        Task DeleteAsync(Guid id);
    }
}
