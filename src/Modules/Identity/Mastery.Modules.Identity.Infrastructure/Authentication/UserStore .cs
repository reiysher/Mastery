using Mastery.Modules.Identity.Domain.Users;
using Mastery.Modules.Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mastery.Modules.Identity.Infrastructure.Authentication;

internal sealed class UserStore(IdentityDbContext dbContext) : IUserStore<User>
{
    public Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(userId);
        return dbContext.Set<User>()
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return dbContext.Set<User>()
            .SingleOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        dbContext.Set<User>().Update(user);
        return Task.FromResult(IdentityResult.Success);
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        dbContext.Set<User>().Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName?.ToUpperInvariant());
    }

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName)!;
    }

    public Task SetNormalizedUserNameAsync(User user, string? normalizedName, CancellationToken cancellationToken)
    {
        user.SetNormalizedUserName(normalizedName);
        return Task.CompletedTask;
    }

    public Task SetUserNameAsync(User user, string? userName, CancellationToken cancellationToken)
    {
        user.SetUserName(userName);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}
