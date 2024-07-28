namespace Mastery.Career.Domain.Jobs;

public static class JobErrors
{
    public static Error NotFound = new("Job.Found", "Job with specified identifier not found");
}
