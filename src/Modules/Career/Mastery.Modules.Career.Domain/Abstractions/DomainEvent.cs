namespace Mastery.Modules.Career.Domain.Abstractions;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccuredOn = DateTimeOffset.UtcNow;
    }

    protected DomainEvent(Guid id, DateTimeOffset occuredOn)
    {
        Id = id;
        OccuredOn = occuredOn;
    }

    public Guid Id { get; init; }

    public DateTimeOffset OccuredOn { get; init; }
}
