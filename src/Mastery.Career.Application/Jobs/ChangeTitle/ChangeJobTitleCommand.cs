using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Jobs.ChangeTitle;

public sealed record ChangeJobTitleCommand(Guid JobId, string? NewTitle) : ICommand;
