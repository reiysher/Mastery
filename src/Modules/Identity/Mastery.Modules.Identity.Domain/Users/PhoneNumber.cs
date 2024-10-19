using System.Text.RegularExpressions;
using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Domain.Identity;

public sealed partial record PhoneNumber
{
    private const string Pattern = "^[0-9]+$";

    public string CountryCode { get; private set; }

    public string Value { get; private set; }

    public bool Confirmed { get; private set; }

    private PhoneNumber() { }

    public static PhoneNumber Parse(string? countryCode, string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(countryCode) || !countryCode.StartsWith('+'))
        {
            throw new InvalidOperationException(UserErrors.InvalidPhoneNumber);
        }

        if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 10 || !PhoneNumberValidationRegex().IsMatch(phoneNumber))
        {
            throw new InvalidOperationException(UserErrors.InvalidPhoneNumber);
        }

        return new PhoneNumber
        {
            CountryCode = countryCode,
            Value = phoneNumber,
            Confirmed = false,
        };
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberValidationRegex();
}
