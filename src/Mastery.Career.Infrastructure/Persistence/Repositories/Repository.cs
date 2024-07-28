﻿using Mastery.Career.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Career.Infrastructure.Persistence.Repositories;

internal abstract class Repository<TAggregate, TId>(ApplicationDbContext dbContext)
    where TAggregate : Aggregate<TId>
    where TId : notnull
{
    protected readonly ApplicationDbContext DbContext = dbContext;

    public async Task<TAggregate?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<TAggregate>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public void Add(TAggregate aggregate)
    {
        DbContext.Add(aggregate);
    }
}
