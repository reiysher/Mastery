using Mastery.Career.Domain.Jobs;

namespace Mastery.Career.Domain.Users;

public sealed record UserId
{
    public Guid Value { get; init; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId New()
    {
        return new UserId(Guid.NewGuid());
    }

    public static UserId From(Guid? value)
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value == Guid.Empty)
        {
            throw new ArgumentException(nameof(value));
        }

        return new UserId(value.Value);
    }

    public static UserId Copy(UserId? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        return new UserId(other.Value);
    }
}
