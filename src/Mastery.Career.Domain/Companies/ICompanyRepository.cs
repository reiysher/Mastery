namespace Mastery.Career.Domain.Companies;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Company>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);

    void Add(Company company);
}
