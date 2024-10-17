using Microsoft.EntityFrameworkCore;

namespace Mastery.Common.Infrastructure.Data;

public interface ISeeder
{
    int Order { get; }

    Task SeedAsync(CancellationToken cancellationToken = default);
}
