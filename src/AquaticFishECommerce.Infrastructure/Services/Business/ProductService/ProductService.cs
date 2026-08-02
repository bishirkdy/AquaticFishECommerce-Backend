using AquaticFishECommerce.Application.Common.Helpers;
using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces.Services.Product;
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

        private List<ProductResponseDto> ApplyDiscount(List<ProductResponseDto> products)
        {
            foreach (var item in products)
            {
                item.DiscountedPrice =
                    Math.Floor(PriceCalculation.GetDiscountedPrice(
                        item.OriginalPrice,
                        item.DiscountPercentage
                    ));
            }
            return products;
        }


        //Service for get all with images
        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllWithImagesAsync();

            var response = _mapper.Map<List<ProductResponseDto>>(products);

            return ApplyDiscount(response);
        }

        //Service for get queriable product with image
        public async Task<IEnumerable<ProductResponseDto>> GetQuariableAsync(ProductQueryDto dto)
        {
            var products = await _productRepository.GetAllProductsAsyncWithImg(dto);

            var response = _mapper.Map<List<ProductResponseDto>>(products);

            return ApplyDiscount(response);
        }

        //Service for get six with image
        public async Task<IEnumerable<ProductResponseDto>> GetSixAsync()
        {
            var products = await _productRepository.GetSixProductAsync();

            var response = _mapper.Map<List<ProductResponseDto>>(products);

            return ApplyDiscount(response);
        }

        //Service for get by id
        public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdWithImagesAsync(id);

            if (product == null)
                return null;

            var response = _mapper.Map<ProductResponseDto>(product);

            response.DiscountedPrice =
               Math.Floor( PriceCalculation.GetDiscountedPrice(
                    response.OriginalPrice,
                    response.DiscountPercentage
                ));

            return response;
        }

    }
}
