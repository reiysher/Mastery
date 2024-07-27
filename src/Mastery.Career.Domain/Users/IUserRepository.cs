namespace Mastery.Career.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);

    void Add(User user);
}
