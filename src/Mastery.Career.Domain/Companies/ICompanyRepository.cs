namespace Mastery.Career.Domain.Companies;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(CompanyId companyId, CancellationToken cancellationToken = default);

    void Add(Company company);
}
