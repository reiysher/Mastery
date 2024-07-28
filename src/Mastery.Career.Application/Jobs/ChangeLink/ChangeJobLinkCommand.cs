using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Jobs.ChangeLink;

public sealed record ChangeJobLinkCommand(Guid JobId, string? NewLink) : ICommand;
