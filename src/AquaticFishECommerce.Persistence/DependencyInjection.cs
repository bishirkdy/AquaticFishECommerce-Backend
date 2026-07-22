using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Infrastructure.Services;
using AquaticFishECommerce.Persistence.Context;
using AquaticFishECommerce.Persistence.Repositories;
using AquaticFishECommerce.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaticFishECommerce.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped(typeof(IGenericRepository<>) , typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<DbInitializer>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            //services.AddScoped<IOrderRepository, OrderRepository>();
            //services.AddScoped<IAddressRepository, AddressRepository>();
            return services;
        }
    }
}
