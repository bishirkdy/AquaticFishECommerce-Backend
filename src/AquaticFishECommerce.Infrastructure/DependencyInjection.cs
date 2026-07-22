using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using AquaticFishECommerce.Application.Interfaces.Services;
using AquaticFishECommerce.Infrastructure.Services;
using AquaticFishECommerce.Application.Common.Settings;
using Microsoft.Extensions.Configuration;

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
            //services.AddScoped<IFavoriteService, FavoriteService>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IAddressService, AddressService>();
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            return services;
        }
    }
}
