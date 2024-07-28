using Mastery.Career.Domain.Categories;

namespace Mastery.Career.Domain.Companies;

public sealed record CompanyCategory
{
    private CompanyCategory() { }

    public required string Value { get; init; }

    public required string Color { get; init; }

    public Guid? CategoryId { get; init; }

    public static CompanyCategory? From(Category? category)
    {
        if (category is null)
        {
            return null;
        }

        return new CompanyCategory
        {
            CategoryId = category.Id,
            Value = category.Value,
            Color = category.Color.Value,
        };
    }
}
