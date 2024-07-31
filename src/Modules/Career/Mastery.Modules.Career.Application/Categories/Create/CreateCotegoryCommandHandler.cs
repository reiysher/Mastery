using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Application.Abstractions.Messaging;
using Mastery.Modules.Career.Domain.Abstractions;
using Mastery.Modules.Career.Domain.Categories;

namespace Mastery.Modules.Career.Application.Categories.Create;

internal sealed class CreateCotegoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCotegoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCotegoryCommand command, CancellationToken cancellationToken)
    {
        Result<Category> result = Category.Create(
            Guid.NewGuid(),
            command.Value,
            command.Color,
            command.Description);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        Category category = result.Value;

        categoryRepository.Insert(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
