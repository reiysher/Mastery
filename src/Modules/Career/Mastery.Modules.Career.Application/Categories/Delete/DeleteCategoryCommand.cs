using Mastery.Modules.Career.Application.Abstractions.Messaging;

namespace Mastery.Modules.Career.Application.Categories.Delete;

public sealed record DeleteCategoryCommand(Guid CategoryId) : ICommand;
