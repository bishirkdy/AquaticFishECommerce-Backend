namespace AquaticFishECommerce.API.Extensions
{
    public static class SwaggerApplicationBuilderExtensions
    {
        //WebApplication represents the running ASP.NET Core web application.
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
