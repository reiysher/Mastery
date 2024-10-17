using Mastery.Common.Infrastructure.Data;
using Mastery.Modules.Identity.Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Seeds;

internal sealed class PermissionsSeeder(IdentityDbContext dbContext, ILogger<PermissionsSeeder> logger) : ISeeder
{
    public int Order => 15;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        foreach (Permission permission in Permission.All)
        {
            bool isExists = await dbContext
                .Set<Permission>()
                .AnyAsync(r => r.Id == permission.Id, cancellationToken);

            if (!isExists)
            {
                logger.LogInformation("Seeding {Permission} Permission.", permission.Code);
                dbContext.Add(permission);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
