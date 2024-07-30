using Mastery.Modules.Career.Domain.Users;

namespace Mastery.Modules.Career.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(CareerDbContext dbContext)
    : Repository<User, Guid>(dbContext), IUserRepository;
