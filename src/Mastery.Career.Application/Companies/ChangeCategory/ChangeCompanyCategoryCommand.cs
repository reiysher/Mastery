using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Companies.ChangeCategory;

public sealed record ChangeCompanyCategoryCommand(Guid CompanyId, Guid CategoryId) : ICommand;
