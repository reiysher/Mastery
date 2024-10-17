using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Mastery.Common.Infrastructure.Authentication;
using Mastery.Modules.Identity.Application.Identity;
using Mastery.Modules.Identity.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Mastery.Modules.Identity.Infrastructure.Authentication;

internal sealed class TokenService(
    IConfiguration configuration,
    TimeProvider timeProvider,
    IOptions<JwtSettings> jwtSettings)
    : ITokenService
{
    private const string ValidationParametersSectionName = "Authentication:TokenValidationParameters";

    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public TokenDto GenerateToken(User user, IReadOnlyCollection<string> userPermissions)
    {
        JwtSecurityToken jwtSecurityToken = CreateToken(user, userPermissions);
        string accessToken = GetAccessToken(jwtSecurityToken);

        return new TokenDto(
            accessToken,
            jwtSecurityToken.ValidTo,
            GenerateRefreshToken(),
            timeProvider.GetUtcNow().AddDays(_jwtSettings.RefreshTokenExpirationInDays));
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters();

        configuration.GetSection(ValidationParametersSectionName).Bind(tokenValidationParameters);
        tokenValidationParameters!.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        tokenValidationParameters.ValidAudience = _jwtSettings.Audience;
        tokenValidationParameters.ValidIssuer = _jwtSettings.Authority;

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

    private JwtSecurityToken CreateToken(User user, IReadOnlyCollection<string> userPermissions)
    {
        DateTimeOffset expires = timeProvider.GetUtcNow().AddMinutes(_jwtSettings.TokenExpirationInMinutes);
        IEnumerable<Claim> authClaims = GetClaims(user, userPermissions);
        SigningCredentials signingCredentials = GetSigningCredentials();

        return new JwtSecurityToken(
            issuer: _jwtSettings.Authority,
            audience: _jwtSettings.Audience,
            expires: expires.DateTime,
            claims: authClaims,
            signingCredentials: signingCredentials);
    }

    private static string GetAccessToken(JwtSecurityToken token)
    {
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
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
