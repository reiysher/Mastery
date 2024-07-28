using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Companies.Create;

public sealed record CreateCompanyCommand(string? Title, string? Note, Guid? CategoryId) : ICommand<Guid>;
