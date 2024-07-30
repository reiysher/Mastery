using Mastery.Modules.Career.Application;
using Mastery.Modules.Career.Presentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mastery.Modules.Career.Infrastructure;

public static class CareerModule
{
    public static IServiceCollection AddCareerModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }

    public static IApplicationBuilder MapCareerModuleEndpoints(this WebApplication app)
    {
        app.MapEndpoints();

        return app;
    }
}
