using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Category;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Category;
using AutoMapper;

namespace AquaticFishECommerce.Infrastructure.Services.Business.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        //Service to take all category
        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }

        //Service to take one category
        public async Task<CategoryResponseDto> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException("Category not found.");

            return _mapper.Map<CategoryResponseDto>(category);
        }
    }
}