namespace Mastery.Modules.Identity.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<string>> GetUserPermissions(
        Guid userId,
        CancellationToken cancellationToken);

    void Insert(User user);
}
