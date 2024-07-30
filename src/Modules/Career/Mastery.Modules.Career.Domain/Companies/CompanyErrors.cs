namespace Mastery.Modules.Career.Domain.Companies;

public static class CompanyErrors
{
    public static readonly Error NotFound = new("Company.Found", "Company with specified identifier not found");
}
