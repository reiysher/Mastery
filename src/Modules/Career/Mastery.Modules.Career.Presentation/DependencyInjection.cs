using Mastery.Modules.Career.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;

namespace Mastery.Modules.Career.Presentation;

public static class DependencyInjection
{
    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        app.MapGroup("api/categories")
            .MapCategoryEndpoints()
            .WithOpenApi();

        app.MapGroup("api/companies")
            .MapCompanyEndpoints()
            .WithOpenApi();

        app.MapGroup("api/jobs")
            .MapJobEndpoints()
            .WithOpenApi();

        app.MapGroup("api/users")
            .MapUserEndpoints()
            .WithOpenApi();

        return app;
    }
}
