using System.Diagnostics.CodeAnalysis;
using Mastery.Modules.Identity.Domain.Permissions;

namespace Mastery.Modules.Identity.Domain.Roles;

public sealed class Role : Aggregate<Guid>
{
    public static readonly Role Administrator = new(
        Guid.Parse("133f765d-22e4-4710-91cf-dec9c4fb8404"),
        "Administrator",
        Permission.All);

    public static readonly Role Basic = new(
        Guid.Parse("6b65ebf5-dc01-4b67-ae25-9daef8bda418"),
        "Basic",
        Permission.All);

    public static readonly Role[] All = [Basic, Administrator];

    private readonly HashSet<RolePermission> _permissions = [];

    public string Name { get; private set; }

    public string NormalizedName { get; private set; }

    public string? Description { get; private set; }

    public IReadOnlyCollection<RolePermission> Permissions => [.. _permissions];

    private Role() { }

    [SetsRequiredMembers]
    private Role(Guid id, string name, Permission[] permissions, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("invalid_role_name");
        }

        Id = id;
        Name = name;
        NormalizedName = name.Normalize();
        Description = description;

        foreach (Permission permission in permissions)
        {
            _permissions.Add(new RolePermission(permission.Id));
        }
    }

    public static Role Create(Guid id, string? name, Permission[] permissions, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("invalid_role_name");
        }

        return new Role(id, name, permissions, description);
    }

    public void SetName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("invalid_role_name");
        }

        Name = name;
        NormalizedName = name.Normalize();
    }

    public void SetNormalizedName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidOperationException("invalid_role_name");
        }

        NormalizedName = name;
    }
}
