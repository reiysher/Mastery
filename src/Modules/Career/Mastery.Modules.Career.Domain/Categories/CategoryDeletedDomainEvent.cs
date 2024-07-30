namespace Mastery.Modules.Career.Domain.Categories;

public sealed record CategoryDeletedDomainEvent(Guid CategoryId) : IDomainEvent;
