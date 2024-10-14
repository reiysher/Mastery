using Mastery.Modules.Identity.Application.Identity.Register;

namespace Mastery.Modules.Identity.Presentation.Users;

public sealed record RegisterUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string CountryCode,
        string PhoneNumber,
        string Password,
        string PasswordConfirm)
{
    public RegisterUserCommand ToCommand()
    {
        return new RegisterUserCommand(
            FirstName,
            LastName,
            Email,
            CountryCode,
            PhoneNumber,
            Password,
            PasswordConfirm);
    }
}
