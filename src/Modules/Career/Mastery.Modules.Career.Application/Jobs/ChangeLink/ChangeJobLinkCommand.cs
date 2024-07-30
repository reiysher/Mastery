using Mastery.Modules.Career.Application.Abstractions.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.ChangeLink;

public sealed record ChangeJobLinkCommand(Guid JobId, string? NewLink) : ICommand;
