namespace Mastery.Modules.Career.Domain.Jobs;

public sealed class JobTitleChangedDomainEvent(Guid jobId, string newTitle) : DomainEvent
{
    public Guid JobId { get; init; } = jobId;

    public string NewTitle { get; init; } = newTitle;
}
