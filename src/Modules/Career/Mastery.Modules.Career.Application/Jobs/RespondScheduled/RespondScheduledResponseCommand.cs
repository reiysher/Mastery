using Mastery.Modules.Career.Application.Abstractions.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.RespondScheduled;

public sealed record RespondScheduledResponseCommand(Guid JobId, Guid ResponseId) : ICommand;
