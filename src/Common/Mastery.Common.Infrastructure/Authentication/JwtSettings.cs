using System.ComponentModel.DataAnnotations;

namespace Mastery.Common.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    [Required, MinLength(10), MaxLength(100)]
    public string Key { get; set; } = default!;

    [Required, Range(1, 15)]
    public int TokenExpirationInMinutes { get; set; }

    [Required, Range(1, 15)]
    public int RefreshTokenExpirationInDays { get; set; }

    [Required, MaxLength(100)]
    public string Audience { get; set; } = default!;

    [Required, MaxLength(100)]
    public string Authority { get; set; } = default!;
}
