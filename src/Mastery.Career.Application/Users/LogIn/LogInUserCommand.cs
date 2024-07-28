using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Users.LogIn;

public sealed record LogInUserCommand(string Email, string Password)
    : ICommand<AccessTokenResponse>;
