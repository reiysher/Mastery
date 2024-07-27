using Mastery.Career.Domain.Companies;

namespace Mastery.Career.Domain.Jobs;

public sealed class Job : Entity<JobId>
{
    private Job() { }

    public CompanyId CompanyId { get; private init; } = default!;

    public string Title { get; private set; } = default!;

    public string Url { get; private set; } = default!;

    public Note? Note { get; private set; }

    private readonly List<JobResponse> _jobResponses = [];

    public IReadOnlyCollection<JobResponse> JobResponses => _jobResponses.AsReadOnly();

    public static Job Create(Company company, Guid id, string title, string url)
    {
        return new Job
        {
            Id = JobId.From(id),
            CompanyId = company.Id,
            Title = title,
            Url = url,
            Note = null,
        };
    }
}
