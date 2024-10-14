using System.Text.Json.Serialization;

namespace Mastery.Modules.Identity.Application.Identity;

public sealed record TokenResponse
{
    public TokenResponse(
        string? accessToken,
        string? refreshToken,
        DateTimeOffset? expiresIn,
        string? error = null,
        string? errorDescription = null)
    {
        TokenType = "Bearer";
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiresIn = expiresIn;
        Error = error;
        ErrorDescription = errorDescription;
    }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; init; }

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; init; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; init; }

    [JsonPropertyName("expires_in")]
    public DateTimeOffset? ExpiresIn { get; init; }

    public string? Error { get; init; }

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; init; }
}
