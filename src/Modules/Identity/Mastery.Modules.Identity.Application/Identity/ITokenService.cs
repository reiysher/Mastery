using System.Security.Claims;
using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Application.Identity;

public interface ITokenService
{
    TokenDto GenerateToken(User user, IReadOnlyCollection<string> userPermissions);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string? token);
}
