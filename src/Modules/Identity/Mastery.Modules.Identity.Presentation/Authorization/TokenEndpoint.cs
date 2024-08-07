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
                CancellationToken cancellationToken) => request.GrantType switch
            {
                TokenRequest.TokenGrantType.Password => Results.Ok(
                    await sender.Send(new GenerateTokenCommand(request.Email, request.Password), cancellationToken)),
                TokenRequest.TokenGrantType.RefreshToken => Results.Ok(
                    await sender.Send(new RefreshTokenCommand(request.AccessToken, request.RefreshToken), cancellationToken)),
                _ => Results.BadRequest(),
            })
            .AllowAnonymous()
            .Produces<TokenResponse>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
            .WithOpenApi()
            .WithTags("Identity");
    }
}
