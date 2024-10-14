using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Application.Identity;

public interface ITokenService
{
    JwtSecurityToken CreateToken(User user, IReadOnlyCollection<string> userPermissions);
    string GetAccessToken(JwtSecurityToken token);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string? token);
}
