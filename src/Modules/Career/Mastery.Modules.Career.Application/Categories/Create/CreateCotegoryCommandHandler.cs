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
        var category = Category.Create(
            Guid.NewGuid(),
            command.Value,
            command.Color,
            command.Description);

        categoryRepository.Insert(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
