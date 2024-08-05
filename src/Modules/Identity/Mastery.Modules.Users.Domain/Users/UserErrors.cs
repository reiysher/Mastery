using Mastery.Common.Domain;

namespace Mastery.Modules.Users.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new("User.Found", "User with specified identifier not found");

    public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "The provided credentials were invalid");

    public static readonly Error InvalidFirstName = new("User.FirstName", "Provided first name is invalid");

    public static readonly Error InvalidLastName = new("User.LastName", "Provided last name is invalid");
}
