using Mastery.Career.Application.Abstractions.Data;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Jobs;

namespace Mastery.Career.Application.Jobs.WriteNote;

internal sealed class WriteJobNoteCommandHandler(
    IJobRepository jobRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<WriteJobNoteCommand>
{
    private readonly IJobRepository jobRepository = jobRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result> Handle(WriteJobNoteCommand command, CancellationToken cancellationToken)
    {
        var job = await jobRepository.GetByIdAsync(command.JobId, cancellationToken);

        if (job is null)
        {
            return Result.Failure(JobErrors.NotFound);
        }

        job.WriteNote(command.Note);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
