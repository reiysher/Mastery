using Mastery.Career.Domain.Users;

namespace Mastery.Career.Application.Users;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}
