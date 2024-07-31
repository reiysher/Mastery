using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Users.LogIn;

public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;
