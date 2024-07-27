namespace Mastery.Career.Domain.Users;

public sealed class User : Entity<UserId>
{
    private User() { }

    public FullName Name { get; private set; } = default!;

    public Email Email { get; private set;} = default!;

    public static User Create(string firstName, string lastName, string email)
    {
        return new User
        {
            Id = UserId.New(),
            Name = FullName.From(firstName, lastName),
            Email = Email.Create(email),
        };
    }
}
