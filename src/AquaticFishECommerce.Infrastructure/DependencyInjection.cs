using AquaticFishECommerce.Application.Common.Settings;
using AquaticFishECommerce.Application.Interfaces.External;
using AquaticFishECommerce.Application.Interfaces.Services.Address;
using AquaticFishECommerce.Application.Interfaces.Services.Analysis;
using AquaticFishECommerce.Application.Interfaces.Services.AuthService;
using AquaticFishECommerce.Application.Interfaces.Services.Cart;
using AquaticFishECommerce.Application.Interfaces.Services.Category;
using AquaticFishECommerce.Application.Interfaces.Services.CategoryService;
using AquaticFishECommerce.Application.Interfaces.Services.Favorite;
using AquaticFishECommerce.Application.Interfaces.Services.Order;
using AquaticFishECommerce.Application.Interfaces.Services.Product;
using AquaticFishECommerce.Application.Interfaces.Services.ReviewService;
using AquaticFishECommerce.Application.Interfaces.Services.User;
using AquaticFishECommerce.Infrastructure.Services.Authentication;
using AquaticFishECommerce.Infrastructure.Services.Business;
using AquaticFishECommerce.Infrastructure.Services.Business.AddressService;
using AquaticFishECommerce.Infrastructure.Services.Business.Analysis;
using AquaticFishECommerce.Infrastructure.Services.Business.AuthService;
using AquaticFishECommerce.Infrastructure.Services.Business.CartService;
using AquaticFishECommerce.Infrastructure.Services.Business.CategoryService;
using AquaticFishECommerce.Infrastructure.Services.Business.OrderServices;
using AquaticFishECommerce.Infrastructure.Services.Business.ProductService;
using AquaticFishECommerce.Infrastructure.Services.Business.ReviewService;
using AquaticFishECommerce.Infrastructure.Services.Business.User;
using AquaticFishECommerce.Infrastructure.Services.Business.UserService;
using AquaticFishECommerce.Infrastructure.Services.Email;
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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAdminCategoryService, AdminCategoryService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IRazorpayService, RazorpayService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IAdminOrderService, AdminOrderService>();
            services.AddScoped<IAdminProductService, AdminProductService>();
            services.AddScoped<IAnalysisService, AnalysisService>();
            //services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            services.Configure<RazorpaySettings>(configuration.GetSection("Razorpay"));
            services.Configure<SmtpSettings>(configuration.GetSection("SMTP"));
            return services;
        }
    }
}
