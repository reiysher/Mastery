using Mastery.Modules.Career.Domain.Jobs;
using Mastery.Modules.Career.Infrastructure.Persistence;

namespace Mastery.Modules.Career.Infrastructure.Persistence.Repositories;

internal sealed class JobRepository(ApplicationDbContext dbContext)
    : Repository<Job, Guid>(dbContext), IJobRepository;
