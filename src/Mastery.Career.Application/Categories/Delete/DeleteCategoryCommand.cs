using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Categories.Delete;

public sealed record DeleteCategoryCommand(Guid CategoryId) : ICommand;
