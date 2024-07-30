using Mastery.Modules.Career.Domain.Companies;

namespace Mastery.Modules.Career.Domain.Jobs;

public sealed class Job : Aggregate<Guid>
{
    public Guid CompanyId { get; private init; }

    public string Title { get; private set; } = default!;

    public string Link { get; private set; } = default!;

    public Note? Note { get; private set; }

    private readonly List<JobResponse> _responses = [];

    public IReadOnlyCollection<JobResponse> Responses => _responses.AsReadOnly();

    private Job() { }

    public static Job Create(Company company, Guid id, string? title, string? link, string? note = "")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(link);

        var job = new Job
        {
            Id = id,
            CompanyId = company.Id,
            Title = title,
            Link = link,
            Note = Note.New(note),
        };

        job.RaiseDomainEvent(new JobCreatedDomainEvent(job.Id));

        return job;
    }

    public void ChangeTitle(string? title)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        Title = title;

        RaiseDomainEvent(new JobTitleChangedDomainEvent(Id, Title));
    }

    public void ChangeLink(string? link)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(link);

        Link = link;

        RaiseDomainEvent(new JobLinkChangedDomainEvent(Id, Link));
    }

    public void WriteNote(string? value)
    {
        Note = Note.New(value);

        RaiseDomainEvent(new JobNoteWrittenDomainEvent(Id, Note.Value));
    }

    public void Respond(DateOnly date)
    {
        var response = JobResponse.Create(
            Guid.NewGuid(),
            Id,
            date,
            ResponseStatus.Delivered);

        _responses.Add(response);

        RaiseDomainEvent(new JobRespondedDomainEvent(Id, response.Id));
    }

    public void RespondScheduledResponse(Guid responseId)
    {
        JobResponse? response = _responses
            .FirstOrDefault(r => r.Id == responseId);

        if (response is null)
        {
            throw new InvalidOperationException();
        }

        response.Deliver();

        RaiseDomainEvent(new JobRespondedDomainEvent(Id, response.Id));
    }

    public void ScheduleRespond(DateOnly date)
    {
        var response = JobResponse.Create(
            Guid.NewGuid(),
            Id,
            date,
            ResponseStatus.Scheduled);

        _responses.Add(response);

        RaiseDomainEvent(new JobRespondScheduledDomainEvent(Id, response.Id));
    }
}
