using Mastery.Modules.Career.Application.Abstractions.Messaging;

namespace Mastery.Modules.Career.Application.Categories.ChangeValue;

public sealed record ChangeCategoryValueCommand(Guid CategoryId, string Value) : ICommand;
