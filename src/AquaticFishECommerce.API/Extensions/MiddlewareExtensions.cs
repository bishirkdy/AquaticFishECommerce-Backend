using AquaticFishECommerce.API.Middlewares;

namespace AquaticFishECommerce.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
