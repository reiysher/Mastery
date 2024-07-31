namespace Mastery.Modules.Career.Domain.Categories;

public sealed class Category : Aggregate<Guid>
{
    public string Value { get; private set; } = default!;

    public Color Color { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public bool IsDeleted { get; private set; }

    private Category() { }

    public static Result<Category> Create(Guid id, string value, string color, string? description = "")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Category>(CategoryErrors.InvalidValue);
        }

        if (string.IsNullOrWhiteSpace(color))
        {
            return Result.Failure<Category>(CategoryErrors.InvalidColor);
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Result.Failure<Category>(CategoryErrors.InvalidDescription);
        }

        var category = new Category
        {
            Id = id,
            Value = value,
            Color = Color.New(color),
            Description = description,
            IsDeleted = false,
        };

        category.Raise(new CategoryCreatedDomainEvent(category.Id));

        return category;
    }

    public void Delete()
    {
        IsDeleted = true;

        Raise(new CategoryDeletedDomainEvent(Id));
    }

    public void ChangeValue(string? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (Value == value)
        {
            return;
        }

        Value = value;

        Raise(new CategoryValueUpdated(Id, value));
    }

    public void ChangeColor(string? color)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(color);

        if (Color.Value == color)
        {
            return;
        }

        Color = Color.New(color);

        Raise(new CategoryColorUpdatedDomainEvent(Id, Color.Value));
    }
}
