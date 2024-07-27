namespace Mastery.Career.Domain.Categories;

public sealed record CategoryDeletedDomainEvent(Guid CategoryId) : IDomainEvent;
