using Microsoft.OpenApi; 
public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        //Name of the security scheme
        const string schemeId = "Bearer";

        services.AddSwaggerGen(options =>
        {
            //To create documentation for swagger
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AquaticFishECommerce API",
                Version = "v1",
            });
            //API uses JWT Bearer Authentication.Without this, Swagger doesn't show the Authorize button.
            options.AddSecurityDefinition(schemeId, new OpenApiSecurityScheme //This describes how authentication works.
            {
                Name = "Authorization", //The HTTP header name.
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", //Specifies the HTTP authentication scheme
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token below."
            });

            //Use previously defined Bearer security scheme when making requests
            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(schemeId, document)] = new List<string>()
            });
        });

        return services;
    }
}