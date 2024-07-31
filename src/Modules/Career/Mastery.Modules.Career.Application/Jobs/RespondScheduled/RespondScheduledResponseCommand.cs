using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.RespondScheduled;

public sealed record RespondScheduledResponseCommand(Guid JobId, Guid ResponseId) : ICommand;
