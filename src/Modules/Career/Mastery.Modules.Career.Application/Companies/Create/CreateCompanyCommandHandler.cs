using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Application.Abstractions.Messaging;
using Mastery.Modules.Career.Domain.Abstractions;
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

        var company = Company.Create(
            Guid.NewGuid(),
            command.Title,
            command.Note,
            category);

        companyRepository.Insert(company);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return company.Id;
    }
}
