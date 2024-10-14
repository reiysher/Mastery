namespace Mastery.Modules.Identity.Domain.Permissions;
public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Permission permission);
}
