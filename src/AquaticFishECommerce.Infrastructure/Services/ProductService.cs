using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Infrastructure.Services
{

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IProductImageRepository _productImageRepository;
        public ProductService(IProductRepository productRepository, IMapper mapper , ICloudinaryService cloudinaryService , IProductImageRepository productImageRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _productImageRepository = productImageRepository;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var product = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponseDto>>(product);
        }

        public async Task<ProductResponseDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto,Stream? stream,string? fileName,bool isPrimary)
        {
            var product = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(product);
            if (stream != null && fileName != null)
            {
                var upload =
                    await _cloudinaryService.UploadAsync(stream, fileName);

                var image = new ProductImage
                {
                    ProductId = product.Id,
                    ImageUrl = upload.ImageUrl,
                    PublicId = upload.PublicId,
                    IsPrimary = isPrimary
                };

                await _productImageRepository.AddAsync(image);
            }
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task UpdateAsync(Guid id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if(product == null)
                throw new Exception("Product not found.");
            _mapper.Map(dto, product);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if(product == null)
                throw new Exception("Product not found.");
            await _productRepository.DeleteAsync(product);
        }
    }
}
