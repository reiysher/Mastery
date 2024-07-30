using Mastery.Modules.Career.Application.Abstractions.Messaging;

namespace Mastery.Modules.Career.Application.Categories.Create;

public sealed record CreateCotegoryCommand(string Value, string Color, string? Description) : ICommand<Guid>;

