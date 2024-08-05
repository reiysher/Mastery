using Mastery.Modules.Users.Domain.Users;

namespace Mastery.Modules.Users.Application.Users;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}
