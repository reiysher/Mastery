using Mastery.Common.Application.Messaging;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Categories;

namespace Mastery.Modules.Career.Application.Categories.ChangeColor;

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
