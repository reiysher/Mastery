using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Users.Domain.Users;
using Mastery.Modules.Users.Application.Abstractions.Data;

namespace Mastery.Modules.Users.Application.Users.Register;

internal sealed class RegisterUserCommandHandler(
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

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
