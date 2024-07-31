using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.Respond;

public sealed record RespondJobCommand(Guid JobId, DateOnly RespondDate) : ICommand;
