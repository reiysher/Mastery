using Mastery.Career.Application.Abstractions.Data;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Companies;

namespace Mastery.Career.Application.Companies.ChangeTitle;

internal sealed class ChangeCompanyTitleCommandHandler(
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeCompanyTitleCommand>
{
    private readonly ICompanyRepository companyRepository = companyRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result> Handle(ChangeCompanyTitleCommand command, CancellationToken cancellationToken)
    {
        var company = await companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

        if (company is null)
        {
            return Result.Failure(CompanyErrors.NotFound);
        }

        company.ChangeTitle(command.Title);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
