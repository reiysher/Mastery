using Mastery.Modules.Career.Application.Users;
using Mastery.Modules.Career.Domain.Users;

namespace Mastery.Modules.Career.Infrastructure.Authentication;

internal sealed class AuthenticationService : IAuthenticationService
{
    public Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
