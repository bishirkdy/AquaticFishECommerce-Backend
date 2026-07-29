using AquaticFishECommerce.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllAsync();
        Task<IEnumerable<ProductResponseDto>> GetQuariableAsync(ProductQueryDto dto);
        Task<IEnumerable<ProductResponseDto>> GetSixAsync();
        Task<ProductResponseDto?> GetByIdAsync(Guid id);

    }
}
