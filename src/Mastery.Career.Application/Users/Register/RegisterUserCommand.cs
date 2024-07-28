﻿using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Users.Register;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password)
    : ICommand<Guid>;
