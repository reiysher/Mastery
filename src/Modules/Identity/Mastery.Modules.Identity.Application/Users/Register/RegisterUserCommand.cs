using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Identity.Application.Users.Register;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password)
    : ICommand<Guid>;
