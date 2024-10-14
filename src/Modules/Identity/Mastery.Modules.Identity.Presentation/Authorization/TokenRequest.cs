using System.Text.Json.Serialization;

namespace Mastery.Modules.Identity.Presentation.Authorization;

public sealed record TokenRequest
{
    [JsonPropertyName("grant_type")]
    public TokenGrantType GrantType { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    [JsonPropertyName("password")]
    public string? Password { get; init; }

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; init; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; init; }

    public enum TokenGrantType
    {
        Password = 1,
        RefreshToken = 2
    }
}
