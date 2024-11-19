using Mastery.Modules.Identity.Domain.Roles;

namespace Mastery.Modules.Identity.Domain.Users;

public sealed class User : Aggregate<Guid>
{
    private readonly HashSet<UserRole> _roles = [];
    private readonly HashSet<UserToken> _tokens = [];

    public FullName Name { get; private set; }

    public Email Email { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public string UserName { get; private set; }

    public string NormalizedUserName { get; private set; }

    public string? PasswordHash { get; private set; }

    public IReadOnlyCollection<UserRole> Roles => [.. _roles];

    public IReadOnlyCollection<UserToken> Tokens => [.. _tokens];

    private User() { }

    public static User Create(
        FullName fullName,
        Email email,
        PhoneNumber phoneNumber)
    {
        ArgumentNullException.ThrowIfNull(fullName);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(phoneNumber);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = fullName,
            Email = email,
            PhoneNumber = phoneNumber,
        };

        user.SetUserName(email.Value);

        user._roles.Add(new UserRole(Role.Basic.Id));

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        return user;
    }

    public void SetUserName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("invalid_user_name");
        }

        UserName = name;
        NormalizedUserName = name.Normalize();
    }

    public void SetNormalizedUserName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("invalid_user_name");
        }

        NormalizedUserName = name;
    }

    public void AddRole(Role role)
    {
        if (_roles.Any(r => r.RoleId == role.Id))
        {
            return;
        }

        _roles.Add(new UserRole(role.Id));
    }

    public void SetPasswordHash(string? passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new InvalidOperationException("invalid_password_hash");
        }

        // use collection for history
        PasswordHash = passwordHash;
    }

    public UserToken? GetLastToken()
    {
        return _tokens.MaxBy(token => token.Created);
    }

    public void SetToken(
        string accessToken,
        string refreshToken,
        DateTimeOffset refreshTokenValidTo,
        DateTimeOffset currentTime)
    {
        UserToken? token = GetLastToken();

        if (token is null)
        {
            token = UserToken.Create(Guid.NewGuid(), currentTime);
            _tokens?.Add(token);
        }

        token.SetToken(accessToken, refreshToken, refreshTokenValidTo);
    }
}
