using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Jobs.WriteNote;

public sealed record WriteJobNoteCommand(Guid JobId, string? Note) : ICommand;
