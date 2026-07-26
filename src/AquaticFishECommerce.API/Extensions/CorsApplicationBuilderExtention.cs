namespace AquaticFishECommerce.API.Extensions
{
    public static class CorsApplicationBuilderExtention
    {
        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors("ReactPolicy");
            return app;
        }
    }
}
