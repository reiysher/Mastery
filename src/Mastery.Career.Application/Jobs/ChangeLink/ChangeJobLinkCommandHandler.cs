using Mastery.Career.Application.Abstractions;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Jobs;

namespace Mastery.Career.Application.Jobs.ChangeLink;

internal sealed class ChangeJobLinkCommandHandler(
    IJobRepository jobRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeJobLinkCommand>
{
    private readonly IJobRepository jobRepository = jobRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangeJobLinkCommand command, CancellationToken cancellationToken)
    {
        var job = await jobRepository.GetByIdAsync(command.JobId, cancellationToken);

        if (job is null)
        {
            return Result.Failure(JobErrors.NotFound);
        }

        job.ChangeLink(command.NewLink);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
