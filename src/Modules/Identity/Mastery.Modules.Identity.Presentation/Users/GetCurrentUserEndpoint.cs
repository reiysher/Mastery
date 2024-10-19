using System.Net.Mime;
using Mastery.Common.Presentation.Endpoints;
using Mastery.Modules.Identity.Application.Identity.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Mastery.Modules.Identity.Presentation.Users;

internal sealed class GetCurrentUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users", async (
                [FromServices] ISender sender,
                CancellationToken cancellationToken) =>
        {
            GetCurrentUserResponse result = await sender.Send(
                new GetCurrentUserQuery(),
                cancellationToken);

            return Results.Ok(result);

        })
            .RequireAuthorization("users:read")
            .Produces<GetCurrentUserResponse>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .WithOpenApi()
            .WithTags("Users")
            .WithDescription("Current user info.")
            .WithSummary("Returns current user info.");
    }
}
