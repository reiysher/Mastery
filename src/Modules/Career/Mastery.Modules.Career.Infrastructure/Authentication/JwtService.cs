using Mastery.Modules.Career.Application.Users;
using Mastery.Modules.Career.Domain.Abstractions;

namespace Mastery.Modules.Career.Infrastructure.Authentication;

internal sealed class JwtService : IJwtService
{
    public Task<Result<string>> GetAccessTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
