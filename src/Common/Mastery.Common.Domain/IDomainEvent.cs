namespace Mastery.Common.Domain;

public interface IDomainEvent
{
    Guid Id { get; }

    DateTimeOffset OccuredOn { get; }
}
