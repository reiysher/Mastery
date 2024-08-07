namespace Mastery.Modules.Career.Domain.Jobs;

public static class JobErrors
{
    public static readonly Error NotFound = new("Job.NotFound", "Job with specified identifier not found");

    public static readonly Error InvalidTitle = new("Job.Title", "Provided title is invalid");

    public static readonly Error InvalidLink = new("Job.Link", "Provided link is invalid");

    public static readonly Error ResponseNotFound = new("Job.Response.NotFound", "JobResponse with specified identifier not found");
}
