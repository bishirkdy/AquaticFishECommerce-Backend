using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Product;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;


namespace AquaticFishECommerce.Infrastructure.Services.Business.ProductService
{
    public class AdminProductService :IAdminProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;
        public AdminProductService(IProductRepository productRepository, IMapper mapper, ICloudinaryService cloudinaryService, IProductImageRepository productImageRepository, ICategoryRepository categoryRepository , IOrderRepository orderRepository , IReviewRepository reviewRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _productImageRepository = productImageRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }

        //Service for add product image and product to database
        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto, Stream? stream, string? fileName, bool isPrimary)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("Category not fount");
            }

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

        //Service for update product and image
        public async Task UpdateAsync(
            Guid id,
            UpdateProductDto dto,
            Stream? imageStream,
            string? fileName,
            bool isPrimary)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException("Product not found.");

            if (dto.Name is not null)
                product.Name = dto.Name;

            if (dto.Description is not null)
                product.Description = dto.Description;

            if (dto.Price.HasValue)
                product.Price = dto.Price.Value;
            if (dto.CostPrice.HasValue)
                product.CostPrice = dto.CostPrice.Value;

            if (dto.Stock.HasValue)
                product.Stock = dto.Stock.Value;

            if (dto.DiscountPercentage.HasValue)
                product.DiscountPercentage = dto.DiscountPercentage.Value;

            if (dto.IsActive.HasValue)
                product.IsActive = dto.IsActive.Value;

            if (dto.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);

                if (category == null)
                    throw new NotFoundException("Category not found.");

                product.CategoryId = dto.CategoryId.Value;
            }

            // Update image if a new one is uploaded
            if (imageStream != null && !string.IsNullOrWhiteSpace(fileName))
            {
                // Get current primary image
                var existingImage = product.Images.FirstOrDefault(x => x.IsPrimary);

                // Delete old Cloudinary image
                if (existingImage != null)
                {
                    await _cloudinaryService.DeleteAsync(existingImage.PublicId);
                }

                // Upload new image
                var uploadResult = await _cloudinaryService.UploadAsync(
                    imageStream,
                    fileName);

                if (existingImage != null)
                {
                    existingImage.ImageUrl = uploadResult.ImageUrl;
                    existingImage.PublicId = uploadResult.PublicId;
                    existingImage.IsPrimary = isPrimary;
                }
                else
                {
                    product.Images.Add(new ProductImage
                    {
                        ImageUrl = uploadResult.ImageUrl,
                        PublicId = uploadResult.PublicId,
                        IsPrimary = isPrimary
                    });
                }
            }
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
        }

        //Service for delete product and image
        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException("Product not found.");

            bool hasOrders = await _orderRepository.HasOrdersAsync(id);

            if (hasOrders)
            {
                product.IsActive = false;
                product.Stock = 0;

                await _productRepository.UpdateAsync(product);
                return;
            }

            // Remove reviews
            await _reviewRepository.DeleteByProductIdAsync(id);

            // Delete Cloudinary images
            foreach (var image in product.Images)
            {
                await _cloudinaryService.DeleteAsync(image.PublicId);
            }

            // Delete product
            await _productRepository.DeleteAsync(product);
        }
    }
}
