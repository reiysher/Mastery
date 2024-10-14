using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Domain.Roles;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Role role);
}
