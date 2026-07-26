using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> ExistsAsync(Guid id);
        Task<Product?> GetByIdWithImagesAsync(Guid id);
        Task<IEnumerable<Product>> GetSixProductAsync();
        Task<IEnumerable<Product>> GetAllWithImagesAsync();
        Task<IEnumerable<Product>> GetAllProductsAsyncWithImg(ProductQueryDto query);
    }
}
