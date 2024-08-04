using Mastery.Common.Domain;

namespace Mastery.Modules.Identity.Domain.Users;

public sealed class User : Aggregate<Guid>
{
    public FullName Name { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    public string IdentityId { get; private set; } = string.Empty;

    private User() { }

    public static Result<User> Create(string firstName, string lastName, string email)
    {
        Result<FullName> fullNameResult = FullName.From(firstName, lastName);
        if (fullNameResult.IsFailure)
        {
            return Result.Failure<User>(fullNameResult.Error);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = fullNameResult.Value,
            Email = Email.Create(email),
        };

        user.Raise(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public Result SetIdentityId(string identityId)
    {
        IdentityId = identityId;

        return Result.Success();
    }
}
