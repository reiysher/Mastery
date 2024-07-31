using Mastery.Common.Domain;
using Mastery.Modules.Career.Application.Categories.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Mastery.Modules.Career.Presentation.Endpoints;

public static class CategoryEndpoints
{
    public static RouteGroupBuilder MapCategoryEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("", async (
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
        });

        return group;
    }
}

public sealed record CreateCotegoryRequest(string Value, string Color, string? Description);
