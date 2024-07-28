using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Companies.ChangeTitle;

public sealed record ChangeCompanyTitleCommand(Guid CompanyId, string? Title) : ICommand;
