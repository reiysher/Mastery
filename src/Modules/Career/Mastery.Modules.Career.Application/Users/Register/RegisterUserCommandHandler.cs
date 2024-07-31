using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Users;

namespace Mastery.Modules.Career.Application.Users.Register;

internal sealed class RegisterUserCommandHandler(
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        Result<User> userResult = User.Create(command.FirstName, command.LastName, command.Email);

        if (userResult.IsFailure)
        {
            return Result.Failure<Guid>(userResult.Error);
        }

        User user = userResult.Value;

        string identityId = await authenticationService.RegisterAsync(
            user,
            command.Password,
            cancellationToken);

        user.SetIdentityId(identityId);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
