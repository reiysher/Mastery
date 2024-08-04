using Mastery.Modules.Identity.Application.Users;
using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    public Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
