using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Jobs.WriteNote;

public sealed record WriteJobNoteCommand(Guid JobId, string? Note) : ICommand;
