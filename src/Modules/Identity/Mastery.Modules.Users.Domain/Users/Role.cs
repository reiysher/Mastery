using System.Diagnostics.CodeAnalysis;
using Mastery.Common.Domain;

namespace Mastery.Modules.Users.Domain.Users;

public sealed class Role : Entity<Guid>
{
    public static readonly Role Administrator = new(Guid.Parse("133f765d-22e4-4710-91cf-dec9c4fb8404"), "Administrator");
    public static readonly Role Basic = new(Guid.Parse("6b65ebf5-dc01-4b67-ae25-9daef8bda418"), "Basic");

    [SetsRequiredMembers]
    private Role(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    private Role() { }

    public string Name { get; private set; }
}
