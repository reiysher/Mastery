using Mastery.Career.Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Career.Infrastructure.Persistence.Repositories;
internal sealed class CompanyRepository(ApplicationDbContext dbContext)
    : Repository<Company, Guid>(dbContext), ICompanyRepository
{
    public async Task<IReadOnlyCollection<Company>> GetByCategoryIdAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Company>()
            .Where(company => company.Category!.CategoryId == categoryId)
            .ToArrayAsync(cancellationToken);
    }
}
