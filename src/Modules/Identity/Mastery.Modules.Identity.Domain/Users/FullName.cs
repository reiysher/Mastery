namespace Mastery.Modules.Identity.Domain.Users;

public sealed record FullName
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    private FullName() { }

    public static FullName From(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new InvalidOperationException(UserErrors.InvalidFirstName);
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new InvalidOperationException(UserErrors.InvalidLastName);
        }

        return new FullName
        {
            FirstName = firstName,
            LastName = lastName,
        };
    }
}
