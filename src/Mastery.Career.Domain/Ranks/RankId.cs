using System.Diagnostics.CodeAnalysis;

namespace Mastery.Career.Domain.Ranks;

public sealed record RankId
{
    public required Guid Value { get; init; }

    [SetsRequiredMembers]
    private RankId(Guid value)
    {
        Value = value;
    }

    public static RankId New()
    {
        return new RankId(Guid.NewGuid());
    }

    public static RankId From(Guid? value)
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value == Guid.Empty)
        {
            throw new ArgumentException(nameof(value));
        }

        return new RankId(value.Value);
    }

    public static RankId Copy(RankId? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        return new RankId(other.Value);
    }
}
