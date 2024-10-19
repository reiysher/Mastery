using System.Text.RegularExpressions;
using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Domain.Identity;

public sealed partial record Email
{
    private const string Pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    private Email() { }

    public string Value { get; private init; }

    public bool Confirmed { get; private set; }

    public static Email Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !EmailValidationRegex().IsMatch(value))
        {
            throw new InvalidOperationException(UserErrors.InvalidEmail);
        }

        return new Email
        {
            Value = value,
            Confirmed = false,
        };
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailValidationRegex();
}
