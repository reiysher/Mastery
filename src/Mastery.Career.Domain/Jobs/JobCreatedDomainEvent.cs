namespace Mastery.Career.Domain.Jobs;

public sealed record JobCreatedDomainEvent(Guid JobId) : IDomainEvent;
