using Mastery.Career.Application.Abstractions;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Categories;

namespace Mastery.Career.Application.Categories.Create;

internal sealed class CreateCotegoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCotegoryCommand, Guid>
{
    private readonly ICategoryRepository categoryRepository = categoryRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(CreateCotegoryCommand command, CancellationToken cancellationToken)
    {
        var category = Category.Create(
            Guid.NewGuid(),
            command.Value,
            command.Color);

        categoryRepository.Add(category);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
