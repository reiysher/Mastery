using Mastery.Common.Infrastructure.Data;
using Mastery.Modules.Identity.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Seeds;

internal sealed class RolesSeeder(IdentityDbContext dbContext, ILogger<RolesSeeder> logger) : ISeeder
{
    public int Order => 10;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        foreach (Role role in Role.All)
        {
            bool isExists = await dbContext
                .Set<Role>()
                .AnyAsync(r => r.Id == role.Id, cancellationToken);

            if (!isExists)
            {
                logger.LogInformation("Seeding {Role} Role.", role.Name);
                await dbContext.AddAsync(role, cancellationToken);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
