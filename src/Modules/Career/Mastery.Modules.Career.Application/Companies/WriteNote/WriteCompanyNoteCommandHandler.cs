using Mastery.Common.Application.Messaging;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Companies;

namespace Mastery.Modules.Career.Application.Companies.WriteNote;

internal sealed class WriteCompanyNoteCommandHandler(
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<WriteCompanyNoteCommand>
{
    public async Task<Result> Handle(WriteCompanyNoteCommand command, CancellationToken cancellationToken)
    {
        Company? company = await companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

        if (company is null)
        {
            return Result.Failure(CompanyErrors.NotFound);
        }

        company.WriteNote(command.Note);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
