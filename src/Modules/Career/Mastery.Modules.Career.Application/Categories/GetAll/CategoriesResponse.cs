namespace Mastery.Modules.Career.Application.Categories.GetAll;

public sealed record CategoriesResponse(IReadOnlyCollection<CategoriesResponse.CategoryItem> Items)
{
    public sealed class CategoryItem
    {
        public required Guid Id { get; init; }

        public required string Value { get; init; }

        public required string Color { get; init; }
    }
}
