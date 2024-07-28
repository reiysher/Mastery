namespace Mastery.Career.Domain.Companies;

public sealed record CompanyNoteWrittenDomainEvent(Guid CompanyId, string Note) : IDomainEvent;
