using Mastery.Modules.Career.Domain.Companies;
using Mastery.Modules.Career.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Modules.Career.Infrastructure.Persistence.Repositories;
internal sealed class CompanyRepository(CareerDbContext dbContext)
    : Repository<Company, Guid>(dbContext), ICompanyRepository
{
    public async Task<IReadOnlyCollection<Company>> GetByCategoryIdAsync(
        Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Company>()
            .Where(company => company.Category!.Id == categoryId)
            .ToArrayAsync(cancellationToken);
    }
}
