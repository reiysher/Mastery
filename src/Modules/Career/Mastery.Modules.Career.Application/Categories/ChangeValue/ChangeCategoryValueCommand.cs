using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Categories.ChangeValue;

public sealed record ChangeCategoryValueCommand(Guid CategoryId, string Value) : ICommand;
