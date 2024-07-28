using Mastery.Career.Application.Abstractions;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Companies;

namespace Mastery.Career.Application.Companies.WriteNote;

internal sealed class WriteCompanyNoteCommandHandler(
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<WriteCompanyNoteCommand>
{
    private readonly ICompanyRepository companyRepository = companyRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result> Handle(WriteCompanyNoteCommand command, CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

        if (company is null)
        {
            return Result.Failure(CompanyErrors.NotFound);
        }

        company.WriteNote(command.Note);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
