
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
            builder.Services.AddApplication().AddPersistence(builder.Configuration).AddInfrastructure(builder.Configuration);
            builder.Services.AddApiService(builder.Configuration);
            var app = builder.Build();

            //CreateScope() creates a temporary Dependency Injection scope.
            using (var scope = app.Services.CreateScope())
            {
                //ServiceProvider is the object that creates and gives you the services you registered in DI.
                var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                await initializer.SeedAsync();
            }

            //Call the configured pipeline extention file
            app.UseApiPipeline();
            app.Run();
        }
    }
}
