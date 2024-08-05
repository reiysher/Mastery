using Mastery.Common.Domain;

namespace Mastery.Modules.Users.Domain.Users;

public sealed record FullName
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    private FullName() { }

    public static Result<FullName> From(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<FullName>(UserErrors.InvalidFirstName);
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            return Result.Failure<FullName>(UserErrors.InvalidLastName);
        }

        return new FullName
        {
            FirstName = firstName,
            LastName = lastName,
        };
    }
}
