namespace Mastery.Career.Domain;

public sealed record CompanyId
{
    public Guid Value { get; init; }

    private CompanyId() { }

    public static CompanyId Random()
    {
        return new CompanyId { Value = Guid.NewGuid() };
    }

    public static CompanyId From(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException(CompanyExceptions.InvalidIdInput);
        }

        return new CompanyId { Value = value };
    }

    public static CompanyId CopyOf(RankId other)
    {
        return new CompanyId { Value = other.Value };
    }
}
