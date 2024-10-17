using System.Net.Mime;
using Mastery.Common.Domain;
using Mastery.Common.Presentation.Endpoints;
using Mastery.Common.Presentation.Results;
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
                            Result<TokenResponse> result = await sender.Send(command, cancellationToken);
                            return result.Match(Results.Ok, ApiResults.Problem);
                        }
                    case TokenRequest.TokenGrantType.RefreshToken:
                        {
                            var command = new RefreshTokenCommand(request.AccessToken, request.RefreshToken);
                            Result<TokenResponse> result = await sender.Send(command, cancellationToken);
                            return result.Match(Results.Ok, ApiResults.Problem);
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
