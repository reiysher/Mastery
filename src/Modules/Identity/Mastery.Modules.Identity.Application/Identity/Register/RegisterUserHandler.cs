using Mastery.Common.Application.Messaging;
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
    public async Task<RegisterUserResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user is not null)
        {
            throw new InvalidOperationException(UserErrors.EmailAlreadyTaken);
        }

        if (command.Password != command.PasswordConfirm)
        {
            throw new InvalidOperationException(UserErrors.ConfirmPasswordIsDifferent);
        }

        var fullName = FullName.From(command.FirstName, command.LastName);
        var email = Email.Parse(command.Email);
        var phoneNumber = PhoneNumber.Parse(command.CountryCode, command.PhoneNumber);


        user = User.Create(
            fullName,
            email,
            phoneNumber);

        string passwordHash = passwordHasher.HashPassword(user, command.Password);
        user.SetPasswordHash(passwordHash);

        userRepository.Insert(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterUserResponse(user.Id.ToString());
    }
}
