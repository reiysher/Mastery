using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Mastery.Common.Infrastructure.Authentication;
using Mastery.Modules.Identity.Application.Identity;
using Mastery.Modules.Identity.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Mastery.Modules.Identity.Infrastructure.Authentication;

internal sealed class TokenService(
    TimeProvider timeProvider,
    IOptions<JwtSettings> jwtSettings)
    : ITokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public JwtSecurityToken CreateToken(User user, IReadOnlyCollection<string> userPermissions)
    {
        DateTimeOffset expires = timeProvider.GetUtcNow().AddMinutes(_jwtSettings.TokenExpirationInMinutes);
        IEnumerable<Claim> authClaims = GetClaims(user, userPermissions);
        SigningCredentials signingCredentials = GetSigningCredentials();

        return new JwtSecurityToken(
            issuer: "http://localhost:8000/identity",
            audience: "account",
            expires: expires.DateTime,
            claims: authClaims,
            signingCredentials: signingCredentials);
    }

    public string GetAccessToken(JwtSecurityToken token)
    {
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        ClaimsPrincipal? principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken? securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("invalid_token");
        }

        return principal;
    }

    private static IEnumerable<Claim> GetClaims(User user, IReadOnlyCollection<string> userPermissions)
    {
        IEnumerable<Claim> permissions = userPermissions
                .Select(permission => new Claim(CustomClaims.Permissions, permission));

        return
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email.Value),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber.Value),
            .. permissions
        ];
    }

    private SigningCredentials GetSigningCredentials()
    {
        byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }
}
