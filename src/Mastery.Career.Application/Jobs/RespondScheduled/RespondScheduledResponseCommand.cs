using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Jobs.RespondScheduled;

public sealed record RespondScheduledResponseCommand(Guid JobId, Guid ResponseId) : ICommand;
