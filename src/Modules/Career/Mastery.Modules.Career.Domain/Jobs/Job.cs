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

    public static Result<Job> Create(Company company, Guid id, string? title, string? link, string? note = "")
    {
        Result<Note> noteResult = Note.New(note);
        if (noteResult.IsFailure)
        {
            return Result.Failure<Job>(noteResult.Error);
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Job>(JobErrors.InvalidTitle);
        }

        if (string.IsNullOrWhiteSpace(link))
        {
            return Result.Failure<Job>(JobErrors.InvalidLink);
        }

        var job = new Job
        {
            Id = id,
            CompanyId = company.Id,
            Title = title,
            Link = link,
            Note = noteResult.Value,
        };

        job.Raise(new JobCreatedDomainEvent(job.Id));

        return job;
    }

    public Result ChangeTitle(string? title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure(JobErrors.InvalidTitle);
        }

        Title = title;

        Raise(new JobTitleChangedDomainEvent(Id, Title));

        return Result.Success();
    }

    public Result ChangeLink(string? link)
    {
        if (string.IsNullOrWhiteSpace(link))
        {
            return Result.Failure<Job>(JobErrors.InvalidLink);
        }

        Link = link;

        Raise(new JobLinkChangedDomainEvent(Id, Link));

        return Result.Success();
    }

    public Result WriteNote(string? value)
    {
        Result<Note> noteResult = Note.New(value);
        if (noteResult.IsFailure)
        {
            return Result.Failure(noteResult.Error);
        }

        Note = noteResult.Value;

        Raise(new JobNoteWrittenDomainEvent(Id, Note.Value));

        return Result.Success();
    }

    public Result Respond(DateOnly date)
    {
        var response = JobResponse.Create(
            Guid.NewGuid(),
            Id,
            date,
            ResponseStatus.Delivered);

        _responses.Add(response);

        Raise(new JobRespondedDomainEvent(Id, response.Id));

        return Result.Success();
    }

    public Result RespondScheduledResponse(Guid responseId)
    {
        JobResponse? response = _responses
            .FirstOrDefault(r => r.Id == responseId);

        if (response is null)
        {
            return Result.Failure(JobErrors.ResponseNotFound);
        }

        response.Deliver();

        Raise(new JobRespondedDomainEvent(Id, response.Id));

        return Result.Success();
    }

    public Result ScheduleRespond(DateOnly date)
    {
        var response = JobResponse.Create(
            Guid.NewGuid(),
            Id,
            date,
            ResponseStatus.Scheduled);

        _responses.Add(response);

        Raise(new JobRespondScheduledDomainEvent(Id, response.Id));

        return Result.Success();
    }
}
