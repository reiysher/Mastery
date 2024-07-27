namespace Mastery.Career.Domain.Categories;

public sealed record CategoryCreatedDomainEvent(Guid CategoryId) : IDomainEvent;
