namespace Mastery.Modules.Career.Domain.Companies;

public sealed class CompanyNoteWrittenDomainEvent(Guid companyId, string note) : DomainEvent
{
    public Guid CompanyId { get; init; } = companyId;

    public string Note { get; init; } = note;
}
