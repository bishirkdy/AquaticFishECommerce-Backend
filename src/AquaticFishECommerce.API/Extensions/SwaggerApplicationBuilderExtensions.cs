namespace AquaticFishECommerce.API.Extensions
{
    public static class SwaggerApplicationBuilderExtensions
    {
        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
    }
}
