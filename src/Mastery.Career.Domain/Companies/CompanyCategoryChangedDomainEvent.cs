﻿namespace Mastery.Career.Domain.Companies;

public sealed record CompanyCategoryChangedDomainEvent(Guid CompanyId, Guid NewCategoryId) : IDomainEvent;
