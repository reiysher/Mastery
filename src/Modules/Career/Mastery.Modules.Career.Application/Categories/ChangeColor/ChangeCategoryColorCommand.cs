using Mastery.Modules.Career.Application.Abstractions.Messaging;

namespace Mastery.Modules.Career.Application.Categories.ChangeColor;

public sealed record ChangeCategoryColorCommand(Guid CategoryId, string? Color) : ICommand;
