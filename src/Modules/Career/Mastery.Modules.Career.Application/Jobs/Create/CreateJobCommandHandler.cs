using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Application.Abstractions.Messaging;
using Mastery.Modules.Career.Domain.Abstractions;
using Mastery.Modules.Career.Domain.Companies;
using Mastery.Modules.Career.Domain.Jobs;

namespace Mastery.Modules.Career.Application.Jobs.Create;

internal sealed class CreateJobCommandHandler(
    IJobRepository jobRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateJobCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateJobCommand command, CancellationToken cancellationToken)
    {
        Company? company = await companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

        if (company is null)
        {
            return Result.Failure<Guid>(CompanyErrors.NotFound);
        }

        var job = Job.Create(
            company,
            Guid.NewGuid(),
            command.Title,
            command.Link,
            command.Note);

        jobRepository.Insert(job);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return job.Id;
    }
}
