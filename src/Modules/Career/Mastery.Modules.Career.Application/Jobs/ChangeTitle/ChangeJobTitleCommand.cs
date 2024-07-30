using Mastery.Modules.Career.Application.Abstractions.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.ChangeTitle;

public sealed record ChangeJobTitleCommand(Guid JobId, string? NewTitle) : ICommand;
