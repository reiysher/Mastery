using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Categories.Update;

public sealed record UpdateCategoryCommand(Guid CategoryId, string? Value, string? Color) : ICommand;
