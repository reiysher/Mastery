using Mastery.Modules.Career.Application.Abstractions.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.Respond;

public sealed record RespondJobCommand(Guid JobId, DateOnly RespondDate) : ICommand;
