using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace AquaticFishECommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}
