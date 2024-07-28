using Mastery.Career.Application.Abstractions;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Companies;
using Mastery.Career.Domain.Jobs;

namespace Mastery.Career.Application.Jobs.Create;

internal sealed class CreateJobCommandHandler(
    IJobRepository jobRepository,
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateJobCommand, Guid>
{
    private readonly IJobRepository jobRepository = jobRepository;
    private readonly ICompanyRepository companyRepository = companyRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(CreateJobCommand command, CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

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

        jobRepository.Add(job);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return job.Id;
    }
}
