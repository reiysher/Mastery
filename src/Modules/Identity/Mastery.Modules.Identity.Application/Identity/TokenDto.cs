namespace Mastery.Modules.Identity.Application.Identity;

public sealed record TokenDto(
    string AccessToken,
    DateTimeOffset AccessTokenExpiredIn,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiredIn);
