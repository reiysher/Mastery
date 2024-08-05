using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Users.Application.Users.Register;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password)
    : ICommand<Guid>;
