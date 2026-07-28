
using AquaticFishECommerce.API.Requests.Product;
using AquaticFishECommerce.API.Validator.Product;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

namespace AquaticFishECommerce.API.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiService(this IServiceCollection service , IConfiguration configuration)
        {
            service.AddSwaggerDocumentation();
            service.AddJwtAuthentification(configuration);
            service.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            service.AddCorsPolicy();

            service.AddScoped<IValidator<CreateProductRequest> , CreateProductRequestValidator>();
            //Whenever a request is received, automatically run the registered FluentValidation validator for the model
            service.AddFluentValidationAutoValidation();
            return service;
        }
    }
}
