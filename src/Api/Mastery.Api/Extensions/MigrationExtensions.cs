using Mastery.Common.Infrastructure.Data;
using Mastery.Modules.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Api.Extensions;

internal static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        //ApplyMigration<CareerDbContext>(scope);
        ApplyMigration<IdentityDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    public static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        IOrderedEnumerable<ISeeder> seeders = scope.ServiceProvider
            .GetServices<ISeeder>()
            .OrderBy(s => s.Order);

        foreach (ISeeder? seeder in seeders)
        {
            await seeder.SeedAsync();
        }
    }
}
