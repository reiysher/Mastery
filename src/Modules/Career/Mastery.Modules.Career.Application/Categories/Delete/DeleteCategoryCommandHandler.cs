using Mastery.Common.Application.Messaging;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Domain.Categories;

namespace Mastery.Modules.Career.Application.Categories.Delete;

internal sealed class DeleteCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Result> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(command.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound);
        }

        category.Delete();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
