using Mastery.Common.Domain;
using Mastery.Modules.Identity.Domain.Identity;
using Mastery.Modules.Identity.Domain.Roles;

namespace Mastery.Modules.Identity.Domain.Users;

public sealed class User : Aggregate<Guid>
{
    private readonly HashSet<UserRole> _roles = [];
    private readonly HashSet<UserToken> _tokens = [];

    public FullName Name { get; private set; } = default!;

    public Email Email { get; private set; } = default!;

    public PhoneNumber PhoneNumber { get; private set; } = default!;

    public string UserName { get; private set; }

    public string NormalizedUserName { get; private set; }

    public string? PasswordHash { get; private set; }

    public IReadOnlyCollection<UserRole> Roles => [.. _roles];

    public IReadOnlyCollection<UserToken> Tokens => [.. _tokens];

    private User() { }

    public static Result<User> Create(
        string firstName,
        string lastName,
        string email,
        string countryCode,
        string phoneNumber)
    {
        Result<FullName> fullNameResult = FullName.From(firstName, lastName);
        if (fullNameResult.IsFailure)
        {
            return Result.Failure<User>(fullNameResult.Error);
        }

        Result<Email> emailResult = Email.Parse(email);
        if (emailResult.IsFailure)
        {
            return Result.Failure<User>(emailResult.Error);
        }

        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Parse(countryCode, phoneNumber);
        if (phoneNumberResult.IsFailure)
        {
            return Result.Failure<User>(phoneNumberResult.Error);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = fullNameResult.Value,
            Email = emailResult.Value,
            PhoneNumber = phoneNumberResult.Value,
        };

        user.SetUserName(emailResult.Value.Value);

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

    public UserToken? GetToken(string loginProvider)
    {
        return _tokens.SingleOrDefault(token => token.LoginProvider == loginProvider);
    }

    public void SetToken(
        string loginProvider,
        string accessToken,
        string refreshToken,
        DateTimeOffset refreshTokenValidTo)
    {
        UserToken? token = _tokens?.SingleOrDefault(token => token.LoginProvider == loginProvider);

        if (token is null)
        {
            token = UserToken.Create(Guid.NewGuid(), loginProvider);
            _tokens?.Add(token);
        }

        token.SetToken(accessToken, refreshToken, refreshTokenValidTo);
    }
}
