namespace Mastery.Modules.Career.Domain.Companies;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Company>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);

    void Insert(Company company);
}
