using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Companies.WriteNote;

public sealed record WriteCompanyNoteCommand(Guid CompanyId, string? Note) : ICommand;
