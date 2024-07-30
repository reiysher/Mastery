namespace Mastery.Modules.Career.Domain.Companies;

public sealed class CompanyCategoryChangedDomainEvent(Guid companyId, Guid? newCategoryId) : DomainEvent
{
    public Guid CompanyId { get; init; } = companyId;

    public Guid? NewCategoryId { get; init; } = newCategoryId;
}
