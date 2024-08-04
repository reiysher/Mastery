using Mastery.Common.Domain;

namespace Mastery.Modules.Identity.Domain.Users;

public sealed class UserCreatedDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
