using AquaticFishECommerce.Application.Common.Settings;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Infrastructure.Services.Authentication;
using AquaticFishECommerce.Infrastructure.Services.Business;
using AquaticFishECommerce.Infrastructure.Services.Payment;
using AquaticFishECommerce.Infrastructure.Services.Storage;
using AquaticFishECommerce.Infrastructure.Settings;
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
            services.AddScoped<IRazorpayService, RazorpayService>();
            services.AddScoped<ICartService, CartService>();
            //services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAddressService, AddressService>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            services.Configure<RazorpaySettings>(configuration.GetSection("Razorpay"));
            return services;
        }
    }
}
