namespace Mastery.Modules.Identity.Domain.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : DomainEvent;
