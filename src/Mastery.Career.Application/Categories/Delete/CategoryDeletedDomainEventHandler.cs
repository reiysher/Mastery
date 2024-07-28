using Mastery.Career.Application.Abstractions.Data;
using Mastery.Career.Domain.Categories;
using Mastery.Career.Domain.Companies;
using MediatR;

namespace Mastery.Career.Application.Categories.Delete;

internal sealed class CategoryDeletedDomainEventHandler(
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork)
    : INotificationHandler<CategoryDeletedDomainEvent>
{
    public async Task Handle(CategoryDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var companies = await companyRepository.GetByCategoryIdAsync(domainEvent.CategoryId, cancellationToken);

        foreach (var company in companies)
        {
            company.ResetCategory();
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
