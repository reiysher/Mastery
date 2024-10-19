using Mastery.Common.Application.Messaging;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Jobs;

namespace Mastery.Modules.Career.Application.Jobs.ScheduleRespond;

internal sealed class ScheduleJobResponseCommandHandler(
    IJobRepository jobRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ScheduleJobResponseCommand>
{
    public async Task<Result> Handle(ScheduleJobResponseCommand command, CancellationToken cancellationToken)
    {
        Job? job = await jobRepository.GetByIdAsync(command.JobId, cancellationToken);

        if (job is null)
        {
            return Result.Failure(JobErrors.NotFound);
        }

        job.ScheduleRespond(command.RespondDate);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
