using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Application.Abstractions.Messaging;
using Mastery.Modules.Career.Domain.Abstractions;
using Mastery.Modules.Career.Domain.Jobs;

namespace Mastery.Modules.Career.Application.Jobs.WriteNote;

internal sealed class WriteJobNoteCommandHandler(
    IJobRepository jobRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<WriteJobNoteCommand>
{
    public async Task<Result> Handle(WriteJobNoteCommand command, CancellationToken cancellationToken)
    {
        Job? job = await jobRepository.GetByIdAsync(command.JobId, cancellationToken);

        if (job is null)
        {
            return Result.Failure(JobErrors.NotFound);
        }

        job.WriteNote(command.Note);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
