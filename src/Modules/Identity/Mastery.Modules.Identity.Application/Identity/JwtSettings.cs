using System.ComponentModel.DataAnnotations;

namespace Mastery.Modules.Identity.Application.Identity;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    [Required, MaxLength(100)]
    public string LoginProvider { get; set; } = default!;

    [Required, MinLength(10), MaxLength(100)]
    public string Key { get; set; } = default!;

    [Required, Range(1, 15)]
    public int TokenExpirationInMinutes { get; set; }

    [Required, Range(1, 15)]
    public int RefreshTokenExpirationInDays { get; set; }
}
