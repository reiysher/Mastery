namespace Mastery.Career.Domain;

public sealed record PlatformRank
{
    public required string Platform { get; init; }

    public required double Value { get; init; }

    public static PlatformRank New(string platform, double value)
    {
        return new PlatformRank
        {
            Platform = platform,
            Value = value,
        };
    }
}
