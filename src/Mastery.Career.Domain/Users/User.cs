namespace Mastery.Career.Domain.Users;

public sealed class User : Aggregate<UserId>
{
    private User() { }

    public FullName Name { get; private set; } = default!;

    public Email Email { get; private set;} = default!;

    public static User Create(string firstName, string lastName, string email)
    {
        var user =  new User
        {
            Id = UserId.New(),
            Name = FullName.From(firstName, lastName),
            Email = Email.Create(email),
        };

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}
