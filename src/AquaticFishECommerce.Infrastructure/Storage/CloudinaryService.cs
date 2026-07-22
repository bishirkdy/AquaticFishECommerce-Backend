using AquaticFishECommerce.Application.DTOs.ProductImage;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Infrastructure.Settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;


namespace AquaticFishECommerce.Infrastructure.Storage
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySettings> options)
        {
            var settings = options.Value;

            var account = new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret);
                _cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResultDto> UploadAsync(Stream stream , string fileName)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = "AquaticFishECommerce"
            };
            var result = await _cloudinary.UploadAsync(uploadParams);

            return new ImageUploadResultDto
            {
                ImageUrl = result.SecureUrl.ToString(),
                PublicId = result.PublicId
            };

        }

        public async Task DeleteAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
