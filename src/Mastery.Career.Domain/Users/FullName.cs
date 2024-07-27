namespace Mastery.Career.Domain.Users;

public sealed record FullName
{
    private FullName() { }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public static FullName From(string firstName, string lastName)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName, nameof(firstName));
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));

        return new FullName
        {
            FirstName = firstName,
            LastName = lastName,
        };
    }
}
