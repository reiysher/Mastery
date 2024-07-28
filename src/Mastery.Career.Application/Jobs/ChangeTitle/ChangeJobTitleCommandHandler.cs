using Mastery.Career.Application.Abstractions;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Jobs;

namespace Mastery.Career.Application.Jobs.ChangeTitle;

internal sealed class ChangeJobTitleCommandHandler(
    IJobRepository jobRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeJobTitleCommand>
{
    private readonly IJobRepository jobRepository = jobRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangeJobTitleCommand command, CancellationToken cancellationToken)
    {
        var job = await jobRepository.GetByIdAsync(command.JobId, cancellationToken);

        if (job is null)
        {
            return Result.Failure(JobErrors.NotFound);
        }

        job.ChangeTitle(command.NewTitle);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
