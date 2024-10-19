using Mastery.Common.Application.Messaging;
using Mastery.Modules.Identity.Application.Abstractions.Data;
using Mastery.Modules.Identity.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Mastery.Modules.Identity.Application.Identity.GenerateToken;

internal sealed class GenerateTokenHandler(
    IUnitOfWork unitOfWork,
    IPasswordHasher<User> passwordHasher,
    ITokenService tokenService,
    IUserRepository userRepository,
    TimeProvider timeProvider)
    : ICommandHandler<GenerateTokenCommand, TokenResponse>
{
    public async Task<TokenResponse> Handle(GenerateTokenCommand command, CancellationToken cancellationToken)
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
