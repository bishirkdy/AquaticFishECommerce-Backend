using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace AquaticFishECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            return services;
        }
    }
}
