﻿namespace Mastery.Modules.Career.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(User user);
}