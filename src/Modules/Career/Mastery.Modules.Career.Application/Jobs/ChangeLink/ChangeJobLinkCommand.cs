using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.ChangeLink;

public sealed record ChangeJobLinkCommand(Guid JobId, string? NewLink) : ICommand;
