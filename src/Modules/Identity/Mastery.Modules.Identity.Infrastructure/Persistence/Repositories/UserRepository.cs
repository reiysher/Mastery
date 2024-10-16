using System.Linq.Expressions;
using Mastery.Common.Infrastructure.Repositories;
using Mastery.Modules.Identity.Domain.Permissions;
using Mastery.Modules.Identity.Domain.Roles;
using Mastery.Modules.Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(IdentityDbContext dbContext)
    : Repository<User, Guid>(dbContext), IUserRepository
{

    public async Task<TDto> GetByIdAsync<TDto>(
        Guid userId,
        Expression<Func<User, TDto>> selector,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Set<User>()
            .Where(user => user.Id == userId)
            .Select(selector)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await dbContext
            .Set<User>()
            .SingleOrDefaultAsync(user => user.Email.Value == email, cancellationToken);
    }

    public async Task<IReadOnlyCollection<string>> GetUserPermissions(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await dbContext
            .Set<User>()
            .Where(user => user.Id == userId)
            .SelectMany(user => user.Roles)
            .Join(
                DbContext.Set<Role>(),
                userRole => userRole.RoleId,
                role => role.Id,
                (userRole, role) => role)
            .SelectMany(role => role.Permissions)
            .Join(
                DbContext.Set<Permission>(),
                rolePermission => rolePermission.PermissionId,
                permission => permission.Id,
                (rolePermission, permission) => permission.Code)
            .Distinct()
            .ToArrayAsync(cancellationToken);
    }
}
