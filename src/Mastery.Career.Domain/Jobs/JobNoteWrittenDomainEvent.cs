namespace Mastery.Career.Domain.Jobs;

public sealed record JobNoteWrittenDomainEvent(Guid JobId, string Note) : IDomainEvent;
