using Mastery.Common.Domain;

namespace Mastery.Modules.Identity.Domain.Identity;

public sealed class UserRegisteredDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
