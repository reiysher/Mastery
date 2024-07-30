namespace Mastery.Modules.Career.Domain.Jobs;

public sealed class JobRespondedDomainEvent(Guid jobId, Guid responseId) : DomainEvent
{
    public Guid JobId { get; init; } = jobId;

    public Guid ResponseId { get; init; } = responseId;
}
