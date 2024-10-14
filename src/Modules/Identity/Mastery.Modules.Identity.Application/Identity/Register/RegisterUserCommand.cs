using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Identity.Application.Identity.Register;

public sealed record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string CountryCode,
        string PhoneNumber,
        string Password,
        string PasswordConfirm)
    : ICommand<RegisterUserResponse>;
