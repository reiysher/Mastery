using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Jobs;

namespace Mastery.Modules.Career.Application.Jobs.Respond;

internal sealed class RespondJobCommandHandler(
    IJobRepository jobRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RespondJobCommand>
{
    public async Task<Result> Handle(RespondJobCommand command, CancellationToken cancellationToken)
    {
        Job? job = await jobRepository.GetByIdAsync(command.JobId, cancellationToken);

        if (job is null)
        {
            return Result.Failure(JobErrors.NotFound);
        }

        job.Respond(command.RespondDate);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
