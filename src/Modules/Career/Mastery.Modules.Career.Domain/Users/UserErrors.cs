namespace Mastery.Modules.Career.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new("User.Found", "User with specified identifier not found");

    public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "The provided credentials were invalid");
}
