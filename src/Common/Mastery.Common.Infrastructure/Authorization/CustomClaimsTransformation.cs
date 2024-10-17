using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
namespace Mastery.Common.Infrastructure.Authorization;

internal sealed class CustomClaimsTransformation(IHttpContextAccessor httpContextAccessor) : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        HttpContext? httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return Task.FromResult(principal);
        }

        string? token = httpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ")[^1];
        if (token == null)
        {
            return Task.FromResult(principal);
        }

        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims.ToList();

        var identity = new ClaimsIdentity(claims, principal.Identity!.AuthenticationType);
        principal.AddIdentity(identity);

        return Task.FromResult(principal);
    }
}
