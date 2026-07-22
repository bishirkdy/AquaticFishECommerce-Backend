using System;
using AquaticFishECommerce.Application.Mappings;
using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AquaticFishECommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            //Instead of manual write - services.AddScoped<IValidator<LoginDto>, LoginValidator>();
            //AddValidatorsFromAssembly fluentValidation scans the assembly and registers:
            //GetExecutingAssembly - Returns the assembly that is currently executing.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
