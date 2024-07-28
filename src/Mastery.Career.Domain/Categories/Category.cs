namespace Mastery.Career.Domain.Categories;

public sealed class Category : Aggregate<Guid>
{
    public string Value { get; private set; } = default!;

    public Color Color { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public bool IsDeleted { get; private set; }

    private Category() { }

    public static Category Create(Guid id, string value, string color, string? description = "")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentNullException.ThrowIfNull(color);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        var category = new Category
        {
            Id = id,
            Value = value,
            Color = Color.New(color),
            Description = description,
            IsDeleted = false,
        };

        category.RaiseDomainEvent(new CategoryCreatedDomainEvent(category.Id));

        return category;
    }

    public void Delete()
    {
        IsDeleted = true;

        RaiseDomainEvent(new CategoryDeletedDomainEvent(Id));
    }

    public void ChangeValue(string? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (Value == value)
        {
            return;
        }

        Value = value;

        RaiseDomainEvent(new CategoryValueUpdated(Id, value));
    }

    public void ChangeColor(string? color)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(color);

        if (Color.Value == color)
        {
            return;
        }

        Color = Color.New(color);

        RaiseDomainEvent(new CategoryColorUpdated(Id, Color.Value));
    }
}
