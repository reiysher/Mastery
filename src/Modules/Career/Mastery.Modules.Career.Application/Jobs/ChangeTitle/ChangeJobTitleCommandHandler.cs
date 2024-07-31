using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Jobs;

namespace Mastery.Modules.Career.Application.Jobs.ChangeTitle;

internal sealed class ChangeJobTitleCommandHandler(
    IJobRepository jobRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeJobTitleCommand>
{
    public async Task<Result> Handle(ChangeJobTitleCommand command, CancellationToken cancellationToken)
    {
        Job? job = await jobRepository.GetByIdAsync(command.JobId, cancellationToken);

        if (job is null)
        {
            return Result.Failure(JobErrors.NotFound);
        }

        job.ChangeTitle(command.NewTitle);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
