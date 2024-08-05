using Mastery.Common.Infrastructure.Repositories;
using Mastery.Modules.Users.Domain.Users;

namespace Mastery.Modules.Users.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(IdentityDbContext dbContext)
    : Repository<User, Guid>(dbContext), IUserRepository;
