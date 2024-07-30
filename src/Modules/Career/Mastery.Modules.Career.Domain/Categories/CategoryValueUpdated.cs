namespace Mastery.Modules.Career.Domain.Categories;

public sealed class CategoryValueUpdated(Guid categoryId, string newValue) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;

    public string NewValue { get; init; } = newValue;
}
