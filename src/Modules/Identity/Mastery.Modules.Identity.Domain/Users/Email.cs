using System.Text.RegularExpressions;

namespace Mastery.Modules.Identity.Domain.Users;

public sealed record Email
{
    private const string Pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    private Email() { }

    public string Value { get; private init; }

    public bool Confirmed { get; private set; }

    public static Email Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, Pattern))
        {
            throw new InvalidOperationException(UserErrors.InvalidEmail);
        }

        return new Email
        {
            Value = value,
            Confirmed = false,
        };
    }
}
