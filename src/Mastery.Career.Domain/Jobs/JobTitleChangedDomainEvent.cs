namespace Mastery.Career.Domain.Jobs;

public sealed record JobTitleChangedDomainEvent(Guid JobId, string NewTitle) : IDomainEvent;
