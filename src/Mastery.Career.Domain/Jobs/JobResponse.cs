namespace Mastery.Career.Domain.Jobs;

public sealed record JobResponse
{
    public DateOnly? Date { get; init; }

    public string? Result { get; init; }
}
