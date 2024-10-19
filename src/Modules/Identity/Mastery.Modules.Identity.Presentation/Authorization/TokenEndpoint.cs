using System.Net.Mime;
using Mastery.Common.Presentation.Endpoints;
using Mastery.Modules.Identity.Application.Identity;
using Mastery.Modules.Identity.Application.Identity.GenerateToken;
using Mastery.Modules.Identity.Application.Identity.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Mastery.Modules.Identity.Presentation.Authorization;

internal sealed class TokenEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("connect/token", async (
                [FromServices] ISender sender,
                [FromBody] TokenRequest request,
                CancellationToken cancellationToken) =>
            {
                switch (request.GrantType)
                {
                    case TokenRequest.TokenGrantType.Password:
                        {
                            var command = new GenerateTokenCommand(request.Email, request.Password);
                            TokenResponse result = await sender.Send(command, cancellationToken);
                            return Results.Ok(result);
                        }
                    case TokenRequest.TokenGrantType.RefreshToken:
                        {
                            var command = new RefreshTokenCommand(request.AccessToken, request.RefreshToken);
                            TokenResponse result = await sender.Send(command, cancellationToken);
                            return Results.Ok(result);
                        }

                }

                return Results.BadRequest();
            }
            )
            .AllowAnonymous()
            .Produces<TokenResponse>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .WithOpenApi()
            .WithTags("Identity");
    }
}
