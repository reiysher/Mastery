using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Categories;
using Mastery.Modules.Career.Domain.Companies;
using MediatR;

namespace Mastery.Modules.Career.Application.Categories.Delete;

internal sealed class CategoryDeletedDomainEventHandler(
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : INotificationHandler<CategoryDeletedDomainEvent>
{
    public async Task Handle(CategoryDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Company> companies = await companyRepository.GetByCategoryIdAsync(domainEvent.CategoryId, cancellationToken);

        foreach (Company company in companies)
        {
            company.ResetCategory();
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
