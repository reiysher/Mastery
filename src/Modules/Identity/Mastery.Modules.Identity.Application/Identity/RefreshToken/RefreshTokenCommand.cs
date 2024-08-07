using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Identity.Application.Identity.RefreshToken;

public sealed record RefreshTokenCommand(
    string? AccessToken,
    string? RefreshToken)
    : ICommand<TokenResponse>;
