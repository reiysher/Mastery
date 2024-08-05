using Mastery.Common.Domain;
using Mastery.Modules.Users.Application.Users;

namespace Mastery.Modules.Users.Infrastructure.Authentication;

internal sealed class JwtService : IJwtService
{
    public Task<Result<string>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
