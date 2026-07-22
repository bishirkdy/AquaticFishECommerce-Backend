using AquaticFishECommerce.Application.DTOs.ProductImage;


namespace AquaticFishECommerce.Application.Interfaces.External
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResultDto> UploadAsync(Stream stream, string fileName);
        Task DeleteAsync(string publicId);
    }
}
