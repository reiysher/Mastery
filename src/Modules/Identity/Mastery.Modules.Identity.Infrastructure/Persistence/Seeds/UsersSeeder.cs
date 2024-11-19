using Mastery.Common.Infrastructure.Data;
using Mastery.Modules.Identity.Domain.Roles;
using Mastery.Modules.Identity.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Seeds;

internal sealed class UsersSeeder(
    IPasswordHasher<User> passwordHasher,
    IdentityDbContext dbContext,
    ILogger<UsersSeeder> logger,
    IOptions<List<DefaultUser>> defaultUsers)
    : ISeeder
{
    private readonly List<DefaultUser> _defaultUsers = defaultUsers.Value;

    public int Order => 20;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        foreach (DefaultUser defaultUser in _defaultUsers)
        {
            if (await dbContext.Set<User>().SingleOrDefaultAsync(u => u.Email!.Value == defaultUser.Email, cancellationToken)
                is not { } user)
            {
                var fullName = FullName.From(defaultUser.FirstName, defaultUser.LastName);
                var email = Email.Parse(defaultUser.Email);
                var phoneNumber = PhoneNumber.Parse(defaultUser.CountryCode, defaultUser.PhoneNumber);

                user = User.Create(
                    fullName,
                    email,
                    phoneNumber);

                logger.LogInformation("Seeding default user [{Email}].", defaultUser.Email);
                user.SetPasswordHash(passwordHasher.HashPassword(user, defaultUser.Password!));

                await dbContext.AddAsync(user, cancellationToken);
            }

            Role[] roles = await dbContext
                .Set<Role>()
                .Where(r => defaultUser.Roles.Contains(r.Name))
                .ToArrayAsync(cancellationToken);

            if (roles.Length > 0)
            {
                foreach (Role role in roles)
                {
                    logger.LogInformation("Assigning {Role} to {User}.", role.Name, defaultUser.Email);
                    user.AddRole(role);
                }
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
