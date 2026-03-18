using System.Text;
using Microsoft.OpenApi;

namespace KitaTrackDemo.Api.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "KitaTrack Demo API", 
                Version = "v1",
                Description = "Backend for managing GCash, Maya or other e-wallet transactions.",
                Contact = new OpenApiContact
                {
                    Name = "Roberto Ubatay Jr",
                    Email = "rubatay.dev@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/roberto-ubatay-jr/")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token only (No 'Bearer' prefix needed)."
            });

            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        });

        return services;
    }
}