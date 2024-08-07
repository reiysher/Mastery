using Mastery.Common.Infrastructure.Repositories;
using Mastery.Modules.Identity.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(IdentityDbContext dbContext)
    : Repository<User, Guid>(dbContext), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<User>()
            .Include(user => user.Roles)
            .SingleOrDefaultAsync(user => user.Email.Value == email, cancellationToken);
    }
}
