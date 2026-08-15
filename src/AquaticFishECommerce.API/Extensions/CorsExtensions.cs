namespace AquaticFishECommerce.API.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicy(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var allowedOrigin =
                Environment.GetEnvironmentVariable(
                    "Cors__AllowedOrigins__0");

            Console.WriteLine($"CORS Origin: {allowedOrigin}");

            services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", policy =>
                {
                    policy
                        .WithOrigins(allowedOrigin ?? "")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }
    }
}