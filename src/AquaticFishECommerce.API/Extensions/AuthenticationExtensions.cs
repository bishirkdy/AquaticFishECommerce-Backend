using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace AquaticFishECommerce.API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentification(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))

                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("AUTH FAILED");
                        Console.WriteLine(context.Exception);
                        return Task.CompletedTask;
                    },

                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("TOKEN VALIDATED");
                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                        Console.WriteLine("CHALLENGE");
                        Console.WriteLine($"Error: {context.Error}");
                        Console.WriteLine($"Description: {context.ErrorDescription}");
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();

            return services;
        }
    }
}
