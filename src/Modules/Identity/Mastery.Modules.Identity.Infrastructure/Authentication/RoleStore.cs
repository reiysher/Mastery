using Mastery.Modules.Identity.Domain.Identity;
using Mastery.Modules.Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Modules.Identity.Infrastructure.Authentication;

internal sealed class RoleStore(IdentityDbContext dbContext) : IRoleStore<Role>
{
    public Task<Role?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(roleId);
        return dbContext.Set<Role>()
            .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public Task<Role?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return dbContext.Set<Role>()
            .SingleOrDefaultAsync(r => r.Name.Normalize() == normalizedRoleName, cancellationToken);
    }

    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(role, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        dbContext.Set<Role>().Update(role);
        return Task.FromResult(IdentityResult.Success);
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        dbContext.Set<Role>().Remove(role);
        await dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<string?> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name?.ToUpperInvariant());
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Id.ToString());
    }

    public Task<string?> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name)!;
    }

    public Task SetNormalizedRoleNameAsync(Role role, string? normalizedName, CancellationToken cancellationToken)
    {
        role.SetNormalizedName(normalizedName);
        return Task.CompletedTask;
    }

    public Task SetRoleNameAsync(Role role, string? roleName, CancellationToken cancellationToken)
    {
        role.SetName(roleName);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}
