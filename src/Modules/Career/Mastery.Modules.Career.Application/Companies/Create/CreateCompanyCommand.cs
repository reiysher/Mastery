﻿using Mastery.Modules.Career.Application.Abstractions.Messaging;

namespace Mastery.Modules.Career.Application.Companies.Create;

public sealed record CreateCompanyCommand(string? Title, string? Note, Guid? CategoryId) : ICommand<Guid>;