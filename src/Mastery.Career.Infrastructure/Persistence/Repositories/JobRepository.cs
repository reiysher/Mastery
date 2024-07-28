using Mastery.Career.Domain.Jobs;

namespace Mastery.Career.Infrastructure.Persistence.Repositories;

internal sealed class JobRepository(ApplicationDbContext dbContext)
    : Repository<Job, Guid>(dbContext), IJobRepository;
