namespace Mastery.Modules.Career.Domain.Companies;

public sealed record CompanyTitle
{
    public string Value { get; private init; }

    private CompanyTitle(string value)
    {
        Value = value;
    }

    public static CompanyTitle From(string? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        return new CompanyTitle(value.Trim());
    }
}
