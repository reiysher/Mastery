namespace Mastery.Career.Domain.Jobs;

public sealed record JobRespondedDomainEvent(Guid JobId, Guid ResponseId) : IDomainEvent;
