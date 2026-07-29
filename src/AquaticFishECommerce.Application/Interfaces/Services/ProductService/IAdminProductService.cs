using AquaticFishECommerce.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Services.Product
{
    public interface IAdminProductService
    {
        Task<ProductResponseDto> CreateAsync(
        CreateProductDto dto,
        Stream? stream,
        string? fileName,
        bool isPrimary);

        Task UpdateAsync(Guid id,
            UpdateProductDto dto,
            Stream? imageStream,
            string? fileName,
            bool isPrimary);

        Task DeleteAsync(Guid id);
    }
}
