namespace Mastery.Modules.Career.Domain.Jobs;

public sealed record JobNoteWrittenDomainEvent(Guid JobId, string Note) : IDomainEvent;
