using AquaticFishECommerce.Application.Common.Exceptions;
using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Product;
using AquaticFishECommerce.Domain.Entities;
using AutoMapper;


namespace AquaticFishECommerce.Infrastructure.Services.Business.ProductService
{

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductService(IProductRepository productRepository, IMapper mapper , ICloudinaryService cloudinaryService , IProductImageRepository productImageRepository , ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _productImageRepository = productImageRepository;
            _categoryRepository = categoryRepository;
        }

        //Service for get all with images
        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var product = await _productRepository.GetAllWithImagesAsync();
            return _mapper.Map<IEnumerable<ProductResponseDto>>(product);
        }

        //Service for get queriable product with image
        public async Task<IEnumerable<ProductResponseDto>> GetQuariableAsync(ProductQueryDto dto)
        {
            var product = await _productRepository.GetAllProductsAsyncWithImg(dto);
            return _mapper.Map<IEnumerable<ProductResponseDto>>(product);
        }

        //Service for get six with image
        public async Task<IEnumerable<ProductResponseDto>> GetSixAsync()
        {
            var product = await _productRepository.GetSixProductAsync();
            return _mapper.Map<IEnumerable<ProductResponseDto>>(product);
        }

        //Service for get by id
        public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdWithImagesAsync(id);
            return _mapper.Map<ProductResponseDto>(product);
        }


   
    }
}
