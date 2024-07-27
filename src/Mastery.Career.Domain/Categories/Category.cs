namespace Mastery.Career.Domain.Categories;

public sealed class Category : Aggregate<CategoryId>
{
    public Category() { }

    public string Value { get; private set; } = default!;

    public Color Color { get; private set; } = default!;

    public static Category Create(Guid id, string value, Color color)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentNullException.ThrowIfNull(color);

        return new Category
        {
            Id = CategoryId.From(id),
            Value = value,
            Color = color,
        };
    }
}
