using Mastery.Common.Domain;

namespace Mastery.Modules.Identity.Domain.Users;

public sealed class UserToken : Entity<Guid>
{

    public string? Name { get; private set; }

    public string? LoginProvider { get; private set; }

    public string? AccessToken { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTimeOffset? RefreshTokenValidTo { get; private set; }

    private UserToken() { }

    public static UserToken Create(Guid id, string loginProvider)
    {
        return new UserToken
        {
            Id = id,
            Name = loginProvider,
            LoginProvider = loginProvider,
        };
    }

    public void SetToken(
        string accessToken,
        string refreshToken,
        DateTimeOffset refreshTokenValidTo)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        RefreshTokenValidTo = refreshTokenValidTo;
    }
}
