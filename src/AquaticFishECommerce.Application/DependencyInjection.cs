using System;
using AquaticFishECommerce.Application.Mappings;
using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AquaticFishECommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services )
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
