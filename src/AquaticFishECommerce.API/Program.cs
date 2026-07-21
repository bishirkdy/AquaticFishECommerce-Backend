
using AquaticFishECommerce.API.Extensions;
using AquaticFishECommerce.Infrastructure;
using AquaticFishECommerce.Application;
using AquaticFishECommerce.Persistence;
using AquaticFishECommerce.Persistence.Seed;
namespace AquaticFishECommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddApplication().AddPersistence(builder.Configuration).AddInfrastructure(builder.Configuration).AddSwaggerDocumentation()
                .AddJwtAuthentification(builder.Configuration)
                .AddControllers();
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                await initializer.SeedAsync();
            }


            app.UseSwaggerDocumentation();
            app.UseExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
