namespace Mastery.Modules.Identity.Infrastructure.Persistence.Seeds;

internal interface ISeeder
{
    int Order { get; }

    Task SeedAsync(IdentityDbContext dbContext, CancellationToken cancellationToken);
}
