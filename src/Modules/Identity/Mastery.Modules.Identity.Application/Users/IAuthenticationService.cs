using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Application.Users;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}
