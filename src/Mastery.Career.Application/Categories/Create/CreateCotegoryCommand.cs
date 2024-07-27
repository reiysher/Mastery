using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Categories.Create;

public sealed record CreateCotegoryCommand(string Value, string Color) : ICommand<Guid>;

