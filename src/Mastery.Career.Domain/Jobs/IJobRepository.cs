namespace Mastery.Career.Domain.Jobs;

public interface IJobRepository
{
    Task<Job?> GetByIdAsync(JobId jobId, CancellationToken cancellationToken = default);

    void Add(Job job);
}
