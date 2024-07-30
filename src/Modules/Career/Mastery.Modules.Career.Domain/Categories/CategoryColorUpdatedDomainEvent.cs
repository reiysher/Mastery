namespace Mastery.Modules.Career.Domain.Categories;

public sealed class CategoryColorUpdatedDomainEvent(Guid categoryId, string newColor) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;

    public string NewColor { get; init; } = newColor;
}
