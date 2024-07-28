using Mastery.Career.Application.Abstractions.Data;
using Mastery.Career.Application.Exceptions;
using Mastery.Career.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Career.Infrastructure.Persistence;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IPublisher publisher)
    : DbContext(options), IUnitOfWork
{
    private readonly IPublisher publisher = publisher;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);

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
        var domainEvents = ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(entry => entry.Entity)
            .SelectMany(aggregate =>
            {
                var domainEvents = aggregate.GetDomainEvents();

                aggregate.ClearDomainEvents();

                return domainEvents;
            })
            .ToArray();

        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}
