namespace Mastery.Modules.Career.Domain.Users;

public sealed class User : Aggregate<Guid>
{
    public FullName Name { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    public string IdentityId { get; private set; } = string.Empty;

    private User() { }

    public static User Create(string firstName, string lastName, string email)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = FullName.From(firstName, lastName),
            Email = Email.Create(email),
        };

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
