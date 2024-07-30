namespace Mastery.Modules.Career.Domain.Jobs;

public sealed class JobResponse : Entity<Guid>
{
    public Guid JobId { get; init; }

    public DateOnly Date { get; private set; }

    public ResponseStatus Status { get; private set; }

    public string Result { get; private set; } = default!;

    private JobResponse() { }

    internal static JobResponse Create(
        Guid id,
        Guid jobId,
        DateOnly date,
        ResponseStatus status = ResponseStatus.None,
        string result = "")
    {
        return new JobResponse
        {
            Id = id,
            JobId = jobId,
            Date = date,
            Status = status,
            Result = result,
        };
    }

    public void Deliver()
    {
        if (Status != ResponseStatus.Scheduled)
        {
            throw new InvalidOperationException();
        }

        Status = ResponseStatus.Delivered;
    }
}
