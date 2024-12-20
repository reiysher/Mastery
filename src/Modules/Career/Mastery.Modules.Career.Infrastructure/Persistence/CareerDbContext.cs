﻿using Mastery.Common.Application.Exceptions;
using Mastery.Common.Domain;
using Mastery.Modules.Career.Application.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Modules.Career.Infrastructure.Persistence;

public sealed class CareerDbContext(
    DbContextOptions<CareerDbContext> options,
    IPublisher publisher)
    : DbContext(options), IUnitOfWork
{
    private readonly IPublisher _publisher = publisher;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("career");

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CareerDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurency exception occurred.", ex);
        }
    }

    private async Task PublishDomainEventsAsync()
    {
        IDomainEvent[] domainEvents = ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(aggregate =>
            {
                IReadOnlyCollection<IDomainEvent> domainEvents = aggregate.DomainEvents;

                aggregate.ClearDomainEvents();

                return domainEvents;
            })
            .ToArray();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
