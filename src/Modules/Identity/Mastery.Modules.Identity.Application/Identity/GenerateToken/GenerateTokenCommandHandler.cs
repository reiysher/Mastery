using System.IdentityModel.Tokens.Jwt;
using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Identity.Application.Abstractions.Data;
using Mastery.Modules.Identity.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Mastery.Modules.Identity.Application.Identity.GenerateToken;

internal sealed class GenerateTokenCommandHandler(
    TimeProvider timeProvider,
    IUnitOfWork unitOfWork,
    IPasswordHasher<User> passwordHasher,
    ITokenService tokenService,
    IUserRepository userRepository,
    IOptions<JwtSettings> jwtSettings)
    : ICommandHandler<GenerateTokenCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(GenerateTokenCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
        {
            throw new InvalidOperationException("authentication_failed");
        }
        
        User user = await userRepository.GetByEmailAsync(command.Email.Trim().Normalize(), cancellationToken)
                    ?? throw new InvalidOperationException("authentication_failed");

        string passwordHash = passwordHasher.HashPassword(user, command.Password);
        PasswordVerificationResult verifyResult = passwordHasher.VerifyHashedPassword(user, passwordHash, command.Password!);

        if (verifyResult == PasswordVerificationResult.Failed)
        {
            throw new InvalidOperationException("authentication_failed");
        }

        JwtSecurityToken token = tokenService.CreateToken(user);

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
