using Mastery.Common.Application.Messaging;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Jobs;

namespace Mastery.Modules.Career.Application.Jobs.RespondScheduled;

internal sealed class RespondScheduledResponseCommandHandler(
    IJobRepository jobRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RespondScheduledResponseCommand>
{
    public async Task<Result> Handle(RespondScheduledResponseCommand command, CancellationToken cancellationToken)
    {
        Job? job = await jobRepository.GetByIdAsync(command.JobId, cancellationToken);

        if (job is null)
        {
            return Result.Failure(JobErrors.NotFound);
        }

        job.RespondScheduledResponse(command.ResponseId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
