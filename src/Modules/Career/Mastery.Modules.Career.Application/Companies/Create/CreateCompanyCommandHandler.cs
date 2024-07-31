using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Categories;
using Mastery.Modules.Career.Domain.Companies;

namespace Mastery.Modules.Career.Application.Companies.Create;

internal sealed class CreateCompanyCommandHandler(
    ICompanyRepository companyRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCompanyCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        Category? category = null;

        if (command.CategoryId is not null)
        {
            category = await categoryRepository.GetByIdAsync(command.CategoryId.Value, cancellationToken);

            if (category is null)
            {
                return Result.Failure<Guid>(CategoryErrors.NotFound);
            }
        }

        Result<Company> companyResult = Company.Create(
            Guid.NewGuid(),
            command.Title,
            command.Note,
            category);

        if (companyResult.IsFailure)
        {
            return Result.Failure<Guid>(companyResult.Error);
        }

        Company company = companyResult.Value;

        companyRepository.Insert(company);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return company.Id;
    }
}
