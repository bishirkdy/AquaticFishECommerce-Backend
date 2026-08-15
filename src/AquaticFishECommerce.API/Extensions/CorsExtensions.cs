namespace AquaticFishECommerce.API.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicy(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var allowedOrigin =
                Environment.GetEnvironmentVariable("Cors__AllowedOrigins__0");

            File.WriteAllText(
                @"C:\inetpub\AquaticFishECommerce\cors-debug.txt",
                $"CORS Origin: {allowedOrigin}"
            );

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