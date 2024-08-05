using Mastery.Modules.Users.Application.Users;
using Mastery.Modules.Users.Domain.Users;

namespace Mastery.Modules.Users.Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    public Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
