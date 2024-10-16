using System.Net.Mime;
using Mastery.Common.Domain;
using Mastery.Common.Presentation.Endpoints;
using Mastery.Common.Presentation.Results;
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
            Result<GetCurrentUserResponse> result = await sender.Send(
                new GetCurrentUserQuery(),
                cancellationToken);

            return result.Match(Results.Ok, ApiResults.Problem);

        })
            .AllowAnonymous()
            .Produces<GetCurrentUserResponse>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .WithOpenApi()
            .WithTags("Users");
    }
}
