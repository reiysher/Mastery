using Mastery.Common.Infrastructure.Repositories;
using Mastery.Modules.Identity.Domain.Permissions;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Repositories;

internal sealed class PermissionRepository(IdentityDbContext dbContext)
    : Repository<Permission, Guid>(dbContext), IPermissionRepository;
