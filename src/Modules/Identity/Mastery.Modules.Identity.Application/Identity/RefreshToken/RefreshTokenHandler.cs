using System.Security.Claims;
using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Identity.Application.Abstractions.Data;
using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Application.Identity.RefreshToken;

internal sealed class RefreshTokenHandler(
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IUserRepository userRepository,
    TimeProvider timeProvider)
    : ICommandHandler<RefreshTokenCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        ClaimsPrincipal userPrincipal = tokenService.GetPrincipalFromExpiredToken(command.AccessToken);
        string userEmail = userPrincipal.FindFirstValue(ClaimTypes.Email);

        User user = await userRepository.GetByEmailAsync(userEmail!, cancellationToken)
                    ?? throw new InvalidOperationException("authentication_failed");

        UserToken? expiredUserToken = user.GetLastToken();

        if (expiredUserToken is null ||
            expiredUserToken.RefreshToken != command.RefreshToken ||
            expiredUserToken.RefreshTokenValidTo <= timeProvider.GetUtcNow())
        {
            throw new InvalidOperationException("invalid_refresh_token");
        }

        IReadOnlyCollection<string> permissions = await userRepository.GetUserPermissions(
            user.Id,
            cancellationToken);

        TokenDto token = tokenService.GenerateToken(user, permissions);

        user.SetToken(
            token.AccessToken,
            token.RefreshToken,
            token.RefreshTokenExpiredIn,
            timeProvider.GetUtcNow());

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new TokenResponse(token.AccessToken, token.RefreshToken, token.AccessTokenExpiredIn);
    }
}
