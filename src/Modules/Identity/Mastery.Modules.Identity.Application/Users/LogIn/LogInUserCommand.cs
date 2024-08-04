using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Identity.Application.Users.LogIn;

public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;
