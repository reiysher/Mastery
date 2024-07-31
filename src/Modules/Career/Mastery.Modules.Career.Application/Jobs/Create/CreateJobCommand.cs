using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.Create;

public sealed record CreateJobCommand(Guid CompanyId, string? Title, string Link, string? Note) : ICommand<Guid>;
