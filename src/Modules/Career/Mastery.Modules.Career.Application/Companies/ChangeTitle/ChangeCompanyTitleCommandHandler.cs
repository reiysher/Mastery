using Mastery.Common.Application.Messaging;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Companies;

namespace Mastery.Modules.Career.Application.Companies.ChangeTitle;

internal sealed class ChangeCompanyTitleCommandHandler(
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeCompanyTitleCommand>
{
    public async Task<Result> Handle(ChangeCompanyTitleCommand command, CancellationToken cancellationToken)
    {
        Company? company = await companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

        if (company is null)
        {
            return Result.Failure(CompanyErrors.NotFound);
        }

        company.ChangeTitle(command.Title);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
