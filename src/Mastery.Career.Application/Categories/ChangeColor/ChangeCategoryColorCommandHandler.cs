using Mastery.Career.Application.Abstractions;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Categories;

namespace Mastery.Career.Application.Categories.ChangeColor;

internal sealed class ChangeCategoryColorCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeCategoryColorCommand>
{
    public async Task<Result> Handle(ChangeCategoryColorCommand command, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound);
        }

        category.ChangeColor(command.Color);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
