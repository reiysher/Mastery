namespace Mastery.Modules.Identity.Domain.Users;

public sealed record UserProfileUpdatedDomainEvent(
    Guid UserId,
    string FirstName,
    string LastName)
    : DomainEvent;
