namespace Mastery.Modules.Career.Domain.Jobs;

public sealed class JobLinkChangedDomainEvent(Guid jobId, string newLink) : DomainEvent
{
    public Guid JobId { get; init; } = jobId;

    public string NewLink { get; init; } = newLink;
}
