using Mastery.Common.Domain;

namespace Mastery.Modules.Identity.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => new("User.NotFound", $"User with specified identifier {userId} not found");

    public static Error NotFound(string? username) => new("User.NotFound", $"User with specified username [{username}] not found");

    public static Error InvalidCredentials => new("User.InvalidCredentials", "The provided credentials were invalid");

    public static Error InvalidFirstName => new("User.FirstName", "Provided first name is invalid");

    public static Error InvalidLastName => new("User.LastName", "Provided last name is invalid");

    public static Error InvalidEmail => new("User.Email", "Provided email is invalid");

    public static Error InvalidPhoneNumber => new("User.PhoneNumber", "Provided phone number is invalid");

    public static Error EmailAlreadyTaken => new("User.Email", "Provided email already taken");

    public static Error ConfirmPasswordIsDifferent => new("User.Password", "Provided password and password configrm is different");
}
