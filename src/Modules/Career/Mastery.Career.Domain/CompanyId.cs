namespace Mastery.Career.Domain;

public sealed record CompanyId
{
    public Guid Value { get; init; }

    public CompanyId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException(CompanyExceptions.InvalidIdInput);
        }

        Value = value;
    }
}
