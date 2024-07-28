using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Categories.ChangeColor;

public sealed record ChangeCategoryColorCommand(Guid CategoryId, string? Color) : ICommand;
