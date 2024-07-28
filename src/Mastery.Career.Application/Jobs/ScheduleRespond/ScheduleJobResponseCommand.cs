using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Jobs.ScheduleRespond;

public sealed record ScheduleJobResponseCommand(Guid JobId, DateOnly RespondDate) : ICommand;
