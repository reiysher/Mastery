namespace Mastery.Modules.Career.Domain.Users;

public sealed record FullName
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    private FullName() { }

    public static FullName From(string firstName, string lastName)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

        return new FullName
        {
            FirstName = firstName,
            LastName = lastName,
        };
    }
}
