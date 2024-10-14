using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Identity.Application.Abstractions.Data;
using Mastery.Modules.Identity.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Mastery.Modules.Identity.Application.Identity.Register;

internal sealed class RegisterUserHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher<User> passwordHasher)
    : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user is not null)
        {
            return Result.Failure<RegisterUserResponse>(UserErrors.EmailAlreadyTaken);
        }

        if (command.Password != command.PasswordConfirm)
        {
            return Result.Failure<RegisterUserResponse>(UserErrors.ConfirmPasswordIsDifferent);
        }

        Result<User> userResult = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.CountryCode,
            command.PhoneNumber);

        user = userResult.Value;

        string passwordHash = passwordHasher.HashPassword(user, command.Password);
        user.SetPasswordHash(passwordHash);

        userRepository.Insert(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new RegisterUserResponse(user.Id.ToString()));
    }
}
