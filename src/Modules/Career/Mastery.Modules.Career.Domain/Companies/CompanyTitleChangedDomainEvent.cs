namespace Mastery.Modules.Career.Domain.Companies;

public sealed class CompanyTitleChangedDomainEvent(Guid companyId, string newTitle) : DomainEvent
{
    public Guid CompanyId { get; init; } = companyId;

    public string NewTitle { get; init; } = newTitle;
}
