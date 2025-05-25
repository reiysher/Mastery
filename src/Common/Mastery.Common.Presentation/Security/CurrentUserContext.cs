using System.Security.Claims;
using Mastery.Common.Application.Security;
using Microsoft.AspNetCore.Http;

namespace Mastery.Common.Presentation.Security;

internal sealed class CurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
{
    public Guid UserId
    {
        get
        {
            if (Guid.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
            {
                return userId;
            }

            throw new InvalidOperationException("User id is not set");
        }
    }
}
