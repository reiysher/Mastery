namespace Mastery.Modules.Career.Domain.Categories;

public sealed class CategoryDeletedDomainEvent(Guid categoryId) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
}
