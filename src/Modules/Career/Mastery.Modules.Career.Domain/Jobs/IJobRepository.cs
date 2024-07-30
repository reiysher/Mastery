namespace Mastery.Modules.Career.Domain.Jobs;

public interface IJobRepository
{
    Task<Job?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Job job);
}
