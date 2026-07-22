namespace AquaticFishECommerce.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        //this is configures the application's HTTP request pipeline
        public static WebApplication UseApiPipeline(this WebApplication app)
        {
            app.UseExceptionMiddleware();
            app.UseSwaggerDocumentation();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }    
    }
}
