using Mastery.Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Modules.Career.Infrastructure.Persistence.Repositories;

internal abstract class Repository<TAggregate, TId>(CareerDbContext dbContext)
    where TAggregate : Aggregate<TId>
    where TId : notnull
{
    protected readonly CareerDbContext DbContext = dbContext;

    public async Task<TAggregate?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<TAggregate>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public void Insert(TAggregate aggregate)
    {
        DbContext.Add(aggregate);
    }
}
