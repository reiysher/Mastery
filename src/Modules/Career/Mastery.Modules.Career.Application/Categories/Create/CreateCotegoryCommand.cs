using FluentValidation;
using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Categories.Create;

public sealed record CreateCotegoryCommand(string Value, string Color, string? Description) : ICommand<Guid>;

internal sealed class CreateCotegoryCommandValidator : AbstractValidator<CreateCotegoryCommand>
{
    public CreateCotegoryCommandValidator()
    {
        RuleFor(c => c.Value).NotEmpty();
        RuleFor(c => c.Color).NotEmpty();
    }
}

