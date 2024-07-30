namespace Mastery.Modules.Career.Domain.Users;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
