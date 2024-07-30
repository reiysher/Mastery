using MediatR;

namespace Mastery.Modules.Career.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTimeOffset OccuredOn { get; }
}
