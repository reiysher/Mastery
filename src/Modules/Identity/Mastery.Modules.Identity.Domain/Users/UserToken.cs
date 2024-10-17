using Mastery.Common.Domain;

namespace Mastery.Modules.Identity.Domain.Users;

public sealed class UserToken : Entity<Guid>
{
    public string? AccessToken { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTimeOffset? RefreshTokenValidTo { get; private set; }

    public DateTimeOffset Created { get; private init; }

    private UserToken() { }

    public static UserToken Create(Guid id, DateTimeOffset currentTime)
    {
        return new UserToken
        {
            Id = id,
            Created = currentTime,
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
