namespace Mastery.Career.Domain.Companies;

public sealed record CompanyTitleChangedDomainEvent(Guid CompanyId, string NewTitle) : IDomainEvent;