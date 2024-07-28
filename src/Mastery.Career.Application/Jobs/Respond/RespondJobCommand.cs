using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Jobs.Respond;

public sealed record RespondJobCommand(Guid JobId, DateOnly RespondDate) : ICommand;
