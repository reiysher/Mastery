using Mastery.Common.Domain;

namespace Mastery.Modules.Users.Application.Users;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
