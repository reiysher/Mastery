namespace Mastery.Career.Domain.Jobs;

public sealed record JobRespondScheduledDomainEvent(Guid JobId, Guid ResponseId) : IDomainEvent;