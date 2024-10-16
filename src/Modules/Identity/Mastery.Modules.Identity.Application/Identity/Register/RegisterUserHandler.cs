using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Identity.Application.Abstractions.Data;
using Mastery.Modules.Identity.Domain.Identity;
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

        Result<FullName> fullNameResult = FullName.From(command.FirstName, command.LastName);
        if (fullNameResult.IsFailure)
        {
            return Result.Failure<RegisterUserResponse>(fullNameResult.Error);
        }

        Result<Email> emailResult = Email.Parse(command.Email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<RegisterUserResponse>(emailResult.Error);
        }

        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Parse(command.CountryCode, command.PhoneNumber);
        if (phoneNumberResult.IsFailure)
        {
            return Result.Failure<RegisterUserResponse>(phoneNumberResult.Error);
        }


        Result<User> userResult = User.Create(
            fullNameResult.Value,
            emailResult.Value,
            phoneNumberResult.Value);

        user = userResult.Value;

        string passwordHash = passwordHasher.HashPassword(user, command.Password);
        user.SetPasswordHash(passwordHash);

        userRepository.Insert(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new RegisterUserResponse(user.Id.ToString()));
    }
}
