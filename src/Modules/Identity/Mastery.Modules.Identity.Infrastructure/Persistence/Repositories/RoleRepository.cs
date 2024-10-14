using Mastery.Common.Infrastructure.Repositories;
using Mastery.Modules.Identity.Domain.Roles;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Repositories;

internal sealed class RoleRepository(IdentityDbContext dbContext)
    : Repository<Role, Guid>(dbContext), IRoleRepository;
