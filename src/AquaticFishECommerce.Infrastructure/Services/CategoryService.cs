using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Category;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;

namespace AquaticFishECommerce.Infrastructure.Services
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

        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }

        public async Task<CategoryResponseDto> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException("Category not found.");

            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
        {
            if (await _categoryRepository.ExistsByNameAsync(dto.Name))
                throw new ConflictException("Category already exists.");

            var category = _mapper.Map<Category>(dto);
            await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryResponseDto>(category);
        }

        public async Task UpdateAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException("Category not found.");

            _mapper.Map(dto, category);
            await _categoryRepository.UpdateAsync(category);
        }


        public async Task DeleteAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException("Category not found.");

            if (await _categoryRepository.HasProductsAsync(id))
                throw new BadRequestException(
                    "Cannot delete a category that contains products.");

            await _categoryRepository.DeleteAsync(category);
        }
    }
}