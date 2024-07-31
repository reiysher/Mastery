using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Categories;
using Mastery.Modules.Career.Domain.Companies;

namespace Mastery.Modules.Career.Application.Companies.ChangeCategory;

internal sealed class ChangeCompanyCategoryCommandHandler(
    ICompanyRepository companyRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeCompanyCategoryCommand>
{
    public async Task<Result> Handle(ChangeCompanyCategoryCommand command, CancellationToken cancellationToken)
    {
        Company? company = await companyRepository.GetByIdAsync(command.CompanyId, cancellationToken);

        if (company is null)
        {
            return Result.Failure(CompanyErrors.NotFound);
        }

        Category? category = await categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound);
        }

        company.ChangeCategory(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
