using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Identity.Application.Abstractions.Data;
using Mastery.Modules.Identity.Domain.Users;
using Microsoft.Extensions.Options;

namespace Mastery.Modules.Identity.Application.Identity.RefreshToken;

internal sealed class RefreshTokenHandler(
    TimeProvider timeProvider,
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IUserRepository userRepository,
    IOptions<JwtSettings> jwtSettings)
    : ICommandHandler<RefreshTokenCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        ClaimsPrincipal userPrincipal = tokenService.GetPrincipalFromExpiredToken(command.AccessToken);
        string userEmail = userPrincipal.FindFirstValue(ClaimTypes.Email);

        User user = await userRepository.GetByEmailAsync(userEmail!, cancellationToken)
                    ?? throw new InvalidOperationException("authentication_failed");

        UserToken? expiredUserToken = user.GetToken(jwtSettings.Value.LoginProvider);

        if (expiredUserToken is null ||
            expiredUserToken.RefreshToken != command.RefreshToken ||
            expiredUserToken.RefreshTokenValidTo <= timeProvider.GetUtcNow())
        {
            throw new InvalidOperationException("invalid_refresh_token");
        }

        IReadOnlyCollection<string> permissions = await userRepository.GetUserPermissions(
            user.Id,
            cancellationToken);

        JwtSecurityToken token = tokenService.CreateToken(user, permissions);

        user.SetToken(
            jwtSettings.Value.LoginProvider,
            tokenService.GetAccessToken(token),
            tokenService.GenerateRefreshToken(),
            timeProvider.GetUtcNow().AddDays(jwtSettings.Value.RefreshTokenExpirationInDays));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        UserToken userToken = user.GetToken(jwtSettings.Value.LoginProvider)
                              ?? throw new InvalidOperationException("authentication_failed");

        return new TokenResponse(userToken.AccessToken, userToken.RefreshToken, token.ValidTo);
    }
}
