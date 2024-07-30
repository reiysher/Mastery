namespace Mastery.Modules.Career.Domain.Users;

public sealed class UserCreatedDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
