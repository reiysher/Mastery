namespace Mastery.Modules.Career.Domain.Jobs;

public static class JobErrors
{
    public static readonly Error NotFound = new("Job.Found", "Job with specified identifier not found");
}
