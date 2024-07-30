namespace Mastery.Modules.Career.Domain.Categories;

public sealed record CategoryCreatedDomainEvent(Guid CategoryId) : IDomainEvent;
