namespace Mastery.Career.Domain;

public sealed class Rank
{
    public required RankId Id { get; init; }

    public string Name { get; private set; } = default!;

    public Color Color { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    private Rank() { }

    public static Rank New(
        RankId id,
        string name,
        Color? color = null,
        string? description = null)
    {
        color ??= Color.Default;
        description ??= string.Empty;

        return new Rank
        {
            Id = id,
            Name = name,
            Color = color,
            Description = description,
        };
    }
}
