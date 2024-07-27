namespace Mastery.Career.Domain.Jobs;

public sealed record JobId
{
    public Guid Value { get; init; }

    private JobId(Guid value)
    {
        Value = value;
    }

    public static JobId New()
    {
        return new JobId(Guid.NewGuid());
    }

    public static JobId From(Guid? value)
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value == Guid.Empty)
        {
            throw new ArgumentException(nameof(value));
        }

        return new JobId(value.Value);
    }

    public static JobId Copy(JobId? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        return new JobId(other.Value);
    }
}
