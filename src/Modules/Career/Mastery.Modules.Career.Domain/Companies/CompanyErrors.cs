namespace Mastery.Modules.Career.Domain.Companies;

public static class CompanyErrors
{
    public static readonly Error NotFound = new("Company.NotFound", "Company with specified identifier not found");

    public static readonly Error InvalidTitle = new("Company.Title", "Provided title is invalid");

    public static readonly Error InvalidCategory = new("Company.Category", "Provided category is invalid");
}
