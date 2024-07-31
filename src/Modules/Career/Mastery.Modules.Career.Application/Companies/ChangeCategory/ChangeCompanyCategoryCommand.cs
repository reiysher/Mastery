using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Companies.ChangeCategory;

public sealed record ChangeCompanyCategoryCommand(Guid CompanyId, Guid CategoryId) : ICommand;
