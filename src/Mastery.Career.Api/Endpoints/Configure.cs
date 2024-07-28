namespace Mastery.Career.Api.Endpoints;

public static class Configure
{
    public static void MapEndpoints(this WebApplication app)
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
    }
}