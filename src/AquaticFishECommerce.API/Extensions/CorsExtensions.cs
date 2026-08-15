namespace AquaticFishECommerce.API.Extensions
{
    public static  class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services , IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

            //Register frondent with policy
            services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", policy =>
                {
                    policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            return services;
        }
    }
}
