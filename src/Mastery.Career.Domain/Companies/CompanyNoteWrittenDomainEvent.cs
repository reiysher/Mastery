namespace Mastery.Career.Domain.Companies;

public sealed record CompanyNoteWrittenDomainEvent(Guid DompanyId, string Note) : IDomainEvent;
