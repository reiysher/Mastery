namespace Mastery.Modules.Career.Domain.Companies;

public sealed record CompanyNoteWrittenDomainEvent(Guid CompanyId, string Note) : IDomainEvent;
