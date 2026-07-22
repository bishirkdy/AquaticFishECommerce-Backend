namespace AquaticFishECommerce.API.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiService(this IServiceCollection service , IConfiguration configuration)
        {
            service.AddSwaggerDocumentation();
            service.AddJwtAuthentification(configuration);
            service.AddControllers();
            return service;
        }
    }
}
