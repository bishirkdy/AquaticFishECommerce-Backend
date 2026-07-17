
using AquaticFishECommerce.API.Extensions;
using AquaticFishECommerce.Infrastructure;
using AquaticFishECommerce.Application;
using AquaticFishECommerce.Persistence;
using Microsoft.Extensions.DependencyInjection;
namespace AquaticFishECommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddApplication().AddPersistence(builder.Configuration).AddInfrastructure().AddSwaggerDocumentation().AddControllers();

            var app = builder.Build();
            app.UseSwaggerDocumentation();

            app.UseExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
