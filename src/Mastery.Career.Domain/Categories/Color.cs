namespace Mastery.Career.Domain.Categories;

public sealed record Color
{
    private Color() { }

    public required string Value { get; init; }

    public static Color New(string value)
    {
        return new Color
        {
            Value = value
        };
    }
}