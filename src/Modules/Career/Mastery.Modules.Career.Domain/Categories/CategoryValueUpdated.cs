namespace Mastery.Modules.Career.Domain.Categories;

public sealed record CategoryValueUpdated(Guid CategoryId, string NewValue) : IDomainEvent;
