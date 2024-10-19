using Mastery.Common.Presentation.Endpoints;
using Mastery.Modules.Career.Application.Categories.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Mastery.Modules.Career.Presentation.Categories;

internal sealed class CreateCategory : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/categories", async (
            CreateCotegoryRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            CreateCotegoryCommand command = new(
                request.Value,
                request.Color,
                request.Description);
            Result<Guid> result = await sender.Send(command, cancellationToken);

            return Results.Ok(result.Value);
        })
            .WithTags("Categories")
            .WithOpenApi();
    }
}

public sealed record CreateCotegoryRequest(string Value, string Color, string? Description);
