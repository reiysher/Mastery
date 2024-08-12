using System.Text.RegularExpressions;
using Mastery.Common.Domain;

namespace Mastery.Modules.Identity.Domain.Identity;

public sealed partial record Email
{
    private const string Pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    
    private Email() { }

    public string Value { get; private init; }

    public bool Confirmed { get; private set; }

    public static Result<Email> Parse(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !EmailValidationRegex().IsMatch(value))
        {
            return Result.Failure<Email>(UserErrors.InvalidEmail);
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
