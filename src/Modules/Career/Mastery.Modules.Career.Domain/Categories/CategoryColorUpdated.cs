namespace Mastery.Modules.Career.Domain.Categories;

public sealed record CategoryColorUpdated(Guid CategoryId, string NewColor) : IDomainEvent;
