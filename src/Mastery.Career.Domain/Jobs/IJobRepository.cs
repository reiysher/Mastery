namespace Mastery.Career.Domain.Jobs;

public interface IJobRepository
{
    Task<Job?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Add(Job job);
}
