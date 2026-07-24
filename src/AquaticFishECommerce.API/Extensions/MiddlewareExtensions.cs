using AquaticFishECommerce.API.Middlewares;

namespace AquaticFishECommerce.API.Extensions
{
    public static class MiddlewareExtensions
    {
        //IApplicationBuilder is an interface used to build the HTTP request pipeline.
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
