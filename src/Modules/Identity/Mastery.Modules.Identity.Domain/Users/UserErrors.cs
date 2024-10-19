namespace Mastery.Modules.Identity.Domain.Users;

public static class UserErrors
{
    public const string NotFoundById = "User with specified identifier not found";

    public const string NotFoundByUsername = "User with specified username not found";

    public const string InvalidCredentials = "The provided credentials were invalid";

    public const string InvalidFirstName = "Provided first name is invalid";

    public const string InvalidLastName = "Provided last name is invalid";

    public const string InvalidEmail = "Provided email is invalid";

    public const string InvalidPhoneNumber = "Provided phone number is invalid";

    public const string EmailAlreadyTaken = "Provided email already taken";

    public const string ConfirmPasswordIsDifferent = "Provided password and password configrm is different";
}
