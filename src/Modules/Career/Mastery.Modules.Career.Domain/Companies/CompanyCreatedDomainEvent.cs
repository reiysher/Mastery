namespace Mastery.Modules.Career.Domain.Companies;

public sealed record CompanyCreatedDomainEvent(Guid CompanyId) : IDomainEvent;
