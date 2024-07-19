namespace Mastery.Career.Domain;

public sealed record RankId
{
    public required Guid Value { get; init; }

    private RankId() { }

    public static RankId Random()
    {
        return new RankId { Value = Guid.NewGuid() };
    }

    public static RankId New(Guid value)
    {
        return new RankId { Value = value };
    }

    public static RankId CopyOf(RankId other)
    {
        return new RankId { Value = other.Value };
    }
}
