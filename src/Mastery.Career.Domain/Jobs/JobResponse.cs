namespace Mastery.Career.Domain.Jobs;

public sealed record JobResponse
{
    public DateTimeOffset? Date { get; init; }

    public string? Result { get; init; }
}
