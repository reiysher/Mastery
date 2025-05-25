using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Mastery.Common.Presentation.OpenApi;

public static class OpenApiConfiguration
{
    public static IServiceCollection AddOpenApiPreConfigured(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddOpenApi();
        services.AddSwaggerGen(options =>
        {
            foreach (Assembly assembly in assemblies)
            {
                options.IncludeXmlComments(assembly);
            }

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                Name = "Authorization",
                Description = "Authorization token"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }});

            options.SwaggerDoc("default", new OpenApiInfo { Title = "Mastery", Version = "default" });
        });

        return services;
    }

    public static IApplicationBuilder MapOpenApiPreConfigured(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "swagger";
            options.SwaggerEndpoint("default/swagger.json", "Mastery - Default");
        });

        return app;
    }
}
