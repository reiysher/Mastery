using Mastery.Career.Domain.Abstractions;

namespace Mastery.Career.Application.Users;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
