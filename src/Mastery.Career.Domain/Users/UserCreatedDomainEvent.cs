namespace Mastery.Career.Domain.Users;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
