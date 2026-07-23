using AquaticFishECommerce.Application.Common.Settings;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Infrastructure.Authentication;
using AquaticFishECommerce.Infrastructure.Services;
using AquaticFishECommerce.Infrastructure.Settings;
using AquaticFishECommerce.Infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AquaticFishECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<ICartService, CartService>();
            //services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IAddressService, AddressService>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            return services;
        }
    }
}
