using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Users;
using MediatR;

namespace Mastery.Career.Application.Users.LogIn;

internal sealed class LogInUserCommandHandler(
    IJwtService jwtService)
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
