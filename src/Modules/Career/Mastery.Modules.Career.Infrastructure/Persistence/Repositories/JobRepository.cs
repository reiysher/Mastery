using Mastery.Common.Infrastructure.Repositories;
using Mastery.Modules.Career.Domain.Jobs;

namespace Mastery.Modules.Career.Infrastructure.Persistence.Repositories;

internal sealed class JobRepository(CareerDbContext dbContext)
    : Repository<Job, Guid>(dbContext), IJobRepository;
