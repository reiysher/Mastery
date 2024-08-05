using Mastery.Modules.Users.Application.Abstractions.Data;
using Mastery.Modules.Users.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Modules.Users.Infrastructure.Persistence;

internal sealed class IdentityDbContext(DbContextOptions<IdentityDbContext> options)
    : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
