using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.ScheduleRespond;

public sealed record ScheduleJobResponseCommand(Guid JobId, DateOnly RespondDate) : ICommand;
