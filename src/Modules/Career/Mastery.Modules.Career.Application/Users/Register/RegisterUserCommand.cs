﻿using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Users.Register;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password)
    : ICommand<Guid>;
