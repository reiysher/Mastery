namespace Mastery.Modules.Career.Domain.Companies;

public sealed class CompanyCreatedDomainEvent(Guid companyId) : DomainEvent
{
    public Guid CompanyId { get; init; } = companyId;
}
