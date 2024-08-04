using Mastery.Common.Infrastructure.Repositories;
using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(IdentityDbContext dbContext)
    : Repository<User, Guid>(dbContext), IUserRepository;
