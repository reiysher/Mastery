namespace Mastery.Modules.Career.Domain.Companies;

public sealed record CompanyTitle
{
    public string Value { get; private init; }

    private CompanyTitle(string value)
    {
        Value = value;
    }

    public static Result<CompanyTitle> From(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<CompanyTitle>(CompanyErrors.InvalidTitle);
        }

        return new CompanyTitle(value.Trim());
    }
}
