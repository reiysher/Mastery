namespace Mastery.Common.Domain;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void ClearDomainEvents();
}
