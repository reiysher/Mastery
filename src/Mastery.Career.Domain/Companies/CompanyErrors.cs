namespace Mastery.Career.Domain.Companies;

public static class CompanyErrors
{
    public static Error NotFound = new("Company.Found", "Company with specified identifier not found");
}
