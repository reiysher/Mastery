using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Identity.Application.Identity.GenerateToken;

public sealed record GenerateTokenCommand(string? Email, string? Password) : ICommand<TokenResponse>;
