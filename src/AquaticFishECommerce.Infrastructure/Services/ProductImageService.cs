//using AquaticFishECommerce.Application.Common.Exceptions;
//using AquaticFishECommerce.Application.DTOs.ProductImage;
//using AquaticFishECommerce.Application.Interfaces.External;
//using AquaticFishECommerce.Application.Interfaces.Repositories;
//using AquaticFishECommerce.Application.Interfaces.Services;
//using AquaticFishECommerce.Domain.Entities;
//using AutoMapper;

//namespace AquaticFishECommerce.Infrastructure.Services
//{
//    public class ProductImageService : IProductImageService
//    {
//        private readonly IProductRepository _productRepository;
//        private readonly IProductImageRepository _productImageRepository;
//        private readonly ICloudinaryService _cloudinaryService;
//        private readonly IMapper _mapper;

//        public ProductImageService( IProductRepository productRepository, IProductImageRepository productImageRepository, ICloudinaryService cloudinaryService,IMapper mapper)
//        {
//            _productRepository = productRepository;
//            _productImageRepository = productImageRepository;
//            _cloudinaryService = cloudinaryService;
//            _mapper = mapper;
//        }
//        //Service to upload product image to cloudinary
//        public async Task<ProductImageResponseDto> CreateAsync( Stream stream, string fileName, Guid productId, bool isPrimary)
//        {
//            var product = await _productRepository.GetByIdAsync(productId);

//            if (product == null)
//                throw new NotFoundException("Product not found.");

//            if (isPrimary)
//            {
//                var primary =
//                    await _productImageRepository.GetPrimaryImageAsync(productId);

//                if (primary != null)
//                {
//                    primary.IsPrimary = false;
//                    await _productImageRepository.UpdateAsync(primary);
//                }
//            }

//            var upload = await _cloudinaryService.UploadAsync(stream, fileName);

//            var image = new ProductImage
//            {
//                ProductId = productId,
//                ImageUrl = upload.ImageUrl,
//                PublicId = upload.PublicId,
//                IsPrimary = isPrimary
//            };

//            await _productImageRepository.AddAsync(image);
//            return _mapper.Map<ProductImageResponseDto>(image);
//        }

//        //Service for delete image from database and cloudinary
//        public async Task DeleteAsync(Guid id)
//        {
//            var image =
//                await _productImageRepository.GetByIdAsync(id);

//            if (image == null)
//                throw new NotFoundException("Image not found.");

//            await _cloudinaryService.DeleteAsync(image.PublicId);
//            await _productImageRepository.DeleteAsync(image);
//        }

//        //Service for get image by product id
//        public async Task<IEnumerable<ProductImageResponseDto>> GetByProductAsync(Guid productId)
//        {
//            var images = await _productImageRepository.GetByProductIdAsync(productId);
//            return _mapper.Map<IEnumerable<ProductImageResponseDto>>(images);
//        }
//    }
//}
