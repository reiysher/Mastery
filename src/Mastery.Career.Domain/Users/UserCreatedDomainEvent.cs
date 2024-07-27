namespace Mastery.Career.Domain.Users;

public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
