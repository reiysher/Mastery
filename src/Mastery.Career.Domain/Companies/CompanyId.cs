namespace Mastery.Career.Domain.Companies;

public sealed record CompanyId
{
    public Guid Value { get; init; }

    private CompanyId(Guid value)
    {
        Value = value;
    }

    public static CompanyId New()
    {
        return new CompanyId(Guid.NewGuid());
    }

    public static CompanyId From(Guid? value)
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value == Guid.Empty)
        {
            throw new ArgumentException(nameof(value));
        }

        return new CompanyId(value.Value);
    }

    public static CompanyId Copy(CompanyId? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        return new CompanyId(other.Value);
    }
}
