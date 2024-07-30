namespace Mastery.Modules.Career.Domain.Jobs;

public sealed class JobNoteWrittenDomainEvent(Guid jobId, string note) : DomainEvent
{
    public Guid JobId { get; init; } = jobId;

    public string Note { get; init; } = note;
}
