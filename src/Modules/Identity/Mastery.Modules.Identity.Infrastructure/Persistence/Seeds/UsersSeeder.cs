using Mastery.Common.Domain;
using Mastery.Modules.Identity.Domain.Identity;
using Mastery.Modules.Identity.Domain.Roles;
using Mastery.Modules.Identity.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mastery.Modules.Identity.Infrastructure.Persistence.Seeds;

internal sealed class UsersSeeder(
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
                Result<FullName> fullNameResult = FullName.From(defaultUser.FirstName, defaultUser.LastName);
                if (fullNameResult.IsFailure)
                {
                    logger.LogInformation("Cannot create user [{Email}].", defaultUser.Email);
                    continue;
                }

                Result<Email> emailResult = Email.Parse(defaultUser.Email);
                if (emailResult.IsFailure)
                {
                    logger.LogInformation("Cannot create user [{Email}].", defaultUser.Email);
                    continue;
                }

                Result<PhoneNumber> phoneNumberResult = PhoneNumber.Parse(defaultUser.CountryCode, defaultUser.PhoneNumber);
                if (phoneNumberResult.IsFailure)
                {
                    logger.LogInformation("Cannot create user [{Email}].", defaultUser.Email);
                    continue;
                }

                Result<User> userResult = User.Create(
                    fullNameResult.Value,
                    emailResult.Value,
                    phoneNumberResult.Value);

                user = userResult.Value;

                if (userResult.IsFailure)
                {
                    logger.LogInformation("Cannot create user [{Email}].", defaultUser.Email);
                    continue;
                }

                logger.LogInformation("Seeding default user [{Email}].", defaultUser.Email);
                var passwordHasher = new PasswordHasher<User>();
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
