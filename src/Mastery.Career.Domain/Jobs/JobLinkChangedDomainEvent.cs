namespace Mastery.Career.Domain.Jobs;

public sealed record JobLinkChangedDomainEvent(Guid JobId, string NewLink) : IDomainEvent;
