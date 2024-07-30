using Mastery.Modules.Career.Domain.Users;

namespace Mastery.Modules.Career.Application.Users;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}
