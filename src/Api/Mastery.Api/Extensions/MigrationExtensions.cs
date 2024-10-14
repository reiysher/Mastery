using Mastery.Modules.Career.Infrastructure.Persistence;
using Mastery.Modules.Identity.Domain.Permissions;
using Mastery.Modules.Identity.Domain.Roles;
using Mastery.Modules.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Api.Extensions;

internal static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<CareerDbContext>(scope);
        ApplyMigration<IdentityDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (context is IdentityDbContext)
        {
            context.AddRange(Permission.All);
            context.AddRange(Role.All);
            context.SaveChanges();
        }
    }
}
