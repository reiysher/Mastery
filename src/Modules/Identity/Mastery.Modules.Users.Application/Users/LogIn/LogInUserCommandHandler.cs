using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Users.Domain.Users;

namespace Mastery.Modules.Users.Application.Users.LogIn;

internal sealed class LogInUserCommandHandler(IJwtService jwtService)
    : ICommandHandler<LogInUserCommand, AccessTokenResponse>
{
    public async Task<Result<AccessTokenResponse>> Handle(LogInUserCommand command, CancellationToken cancellationToken)
    {
        Result<string> result = await jwtService.GetAccessTokenAsync(
            command.Email,
            command.Password,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);
        }

        return new AccessTokenResponse(result.Value);
    }
}
