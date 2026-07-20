using AquaticFishECommerce.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllAsync();
        Task<ProductResponseDto?> GetByIdAsync(Guid id);
        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);
        Task UpdateAsync(Guid id, UpdateProductDto dto);
        Task DeleteAsync(Guid id);
    }
}
