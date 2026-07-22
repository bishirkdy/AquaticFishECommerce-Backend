using AquaticFishECommerce.Application.DTOs.ProductImage;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services
{
    public interface IProductImageService
    {
        Task<ProductImageResponseDto> CreateAsync(Stream stream, string fileName, Guid productId, bool isPrimary);
        Task<IEnumerable<ProductImageResponseDto>> GetByProductAsync(Guid productId);
        Task DeleteAsync(Guid id);
    }
}
