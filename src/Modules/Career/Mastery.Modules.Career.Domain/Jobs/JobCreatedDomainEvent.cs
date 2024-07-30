namespace Mastery.Modules.Career.Domain.Jobs;

public sealed class JobCreatedDomainEvent(Guid jobId) : DomainEvent
{
    public Guid JobId { get; init; } = jobId;
}
