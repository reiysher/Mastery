namespace Mastery.Career.Domain.Categories;

public sealed record CategoryColorUpdated(Guid CategoryId, string NewColor) : IDomainEvent;
