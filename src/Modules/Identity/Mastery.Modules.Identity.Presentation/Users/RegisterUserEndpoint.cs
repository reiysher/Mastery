using System.Net.Mime;
using Mastery.Common.Presentation.Endpoints;
using Mastery.Modules.Identity.Application.Identity.Register;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Mastery.Modules.Identity.Presentation.Users;

internal sealed class RegisterUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (
                [FromServices] ISender sender,
                [FromBody] RegisterUserRequest request,
                CancellationToken cancellationToken) =>
                {
                    RegisterUserResponse result = await sender.Send(
                        request.ToCommand(),
                        cancellationToken);

                    return Results.Ok(result);

                })
            .AllowAnonymous()
            .Produces<RegisterUserResponse>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
            .WithOpenApi()
            .WithTags("Users");
    }
}
