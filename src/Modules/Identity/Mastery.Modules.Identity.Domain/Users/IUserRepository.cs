using System.Linq.Expressions;

namespace Mastery.Modules.Identity.Domain.Users;

public interface IUserRepository
{
    Task<TDto> GetByIdAsync<TDto>(
        Guid userId,
        Expression<Func<User, TDto>> selector,
        CancellationToken cancellationToken);

    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<string>> GetUserPermissions(
        Guid userId,
        CancellationToken cancellationToken);

    void Insert(User user);
}
