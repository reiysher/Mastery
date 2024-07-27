namespace Mastery.Career.Domain.Categories;

public sealed record CategoryId
{
    public Guid Value { get; init; }

    private CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId New()
    {
        return new CategoryId(Guid.NewGuid());
    }

    public static CategoryId From(Guid? value)
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value == Guid.Empty)
        {
            throw new ArgumentException(nameof(value));
        }

        return new CategoryId(value.Value);
    }

    public static CategoryId Copy(CategoryId? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        return new CategoryId(other.Value);
    }
}
