namespace Mastery.Career.Domain.Companies;

public sealed record CompanyCreatedDomainEvent(Guid CompanyId) : IDomainEvent;
