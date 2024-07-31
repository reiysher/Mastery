using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Categories.Delete;

public sealed record DeleteCategoryCommand(Guid CategoryId) : ICommand;
