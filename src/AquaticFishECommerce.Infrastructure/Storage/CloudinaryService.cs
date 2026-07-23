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
            var settings = options.Value; //Actual CloudinarySettings object
            //Configures the Cloudinary client
            var account = new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret);
                _cloudinary = new Cloudinary(account); //Connection to the Cloudinary API
        }

        //Method to upload image to cloudinary
        public async Task<ImageUploadResultDto> UploadAsync(Stream stream , string fileName)
        {
            //ImageUploadParams is a Cloudinary SDK class - It contains all the settings needed for uploading an image.
            var uploadParams = new ImageUploadParams
            {
                //FileDescription tells cloudinary file name and file data
                File = new FileDescription(fileName, stream),
                Folder = "AquaticFishECommerce",
            };
            //Uploaded to cloudinary
            var result = await _cloudinary.UploadAsync(uploadParams);

            return new ImageUploadResultDto
            {
                ImageUrl = result.SecureUrl.ToString(), //SecureUrl is HTTPS URL of the uploaded image and it is a Uri object.
                PublicId = result.PublicId //This is the unique identifier Cloudinary assigns to the uploaded image
            };

        }

        public async Task DeleteAsync(string publicId)
        {
            //DeletionParams is a class from the Cloudinary .NET SDK.
            //It contains the information Cloudinary needs to delete an image
            var deleteParams = new DeletionParams(publicId);
            //DestroyAsync() is a Cloudinary SDK method that sends a request to Cloudinary to delete the specified asset
            await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
