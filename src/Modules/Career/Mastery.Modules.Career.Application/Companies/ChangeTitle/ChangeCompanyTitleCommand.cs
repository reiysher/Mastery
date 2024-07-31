using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Companies.ChangeTitle;

public sealed record ChangeCompanyTitleCommand(Guid CompanyId, string? Title) : ICommand;
