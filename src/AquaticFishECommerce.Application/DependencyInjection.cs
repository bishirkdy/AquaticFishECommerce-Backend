using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AquaticFishECommerce.Application.Mappings;
using FluentValidation;
using System.Reflection;
using AquaticFishECommerce.Application.Common.Settings;

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
