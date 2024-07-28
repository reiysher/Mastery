using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Categories.ChangeValue;

public sealed record ChangeCategoryValueCommand(Guid CategoryId, string Value) : ICommand;
