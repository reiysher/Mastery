using Mastery.Career.Application.Abstractions;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Categories;
using Mastery.Career.Domain.Companies;

namespace Mastery.Career.Application.Companies.Create;

internal sealed class CreateCompanyCommandHandler(
    ICompanyRepository companyRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCompanyCommand, Guid>
{
    private readonly ICompanyRepository companyRepository = companyRepository;
    private readonly ICategoryRepository categoryRepository = categoryRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

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

        companyRepository.Add(company);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return company.Id;
    }
}
