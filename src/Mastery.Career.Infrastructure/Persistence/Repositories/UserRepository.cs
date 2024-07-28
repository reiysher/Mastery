﻿using Mastery.Career.Domain.Users;

namespace Mastery.Career.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(ApplicationDbContext dbContext)
    : Repository<User, Guid>(dbContext), IUserRepository;
