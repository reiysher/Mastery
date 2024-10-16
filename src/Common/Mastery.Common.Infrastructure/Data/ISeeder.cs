using Microsoft.EntityFrameworkCore;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Seeds;

public interface ISeeder
{
    int Order { get; }

    Task SeedAsync(CancellationToken cancellationToken = default);
}
