using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Jobs.Create;

public sealed record CreateJobCommand(Guid CompanyId, string? Title, string Link, string? Note) : ICommand<Guid>;
