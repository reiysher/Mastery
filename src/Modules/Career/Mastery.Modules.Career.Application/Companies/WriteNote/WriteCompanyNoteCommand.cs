using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Companies.WriteNote;

public sealed record WriteCompanyNoteCommand(Guid CompanyId, string? Note) : ICommand;
