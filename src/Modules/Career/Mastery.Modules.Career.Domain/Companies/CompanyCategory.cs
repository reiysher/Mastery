using Mastery.Modules.Career.Domain.Categories;

namespace Mastery.Modules.Career.Domain.Companies;

public sealed record CompanyCategory
{
    private CompanyCategory() { }

    public Guid? Id { get; init; }

    public required string Value { get; init; }

    public required string Color { get; init; }

    public static CompanyCategory? From(Category? category)
    {
        if (category is null)
        {
            return null;
        }

        return new CompanyCategory
        {
            Id = category.Id,
            Value = category.Value,
            Color = category.Color.Value,
        };
    }

    public static CompanyCategory? Default()
    {
        return null;
    }
}
