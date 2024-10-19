using System.Diagnostics.CodeAnalysis;

namespace Mastery.Modules.Identity.Domain.Permissions;

public sealed class Permission : Aggregate<Guid>
{
    public static readonly Permission UsersRead = new(Guid.Parse("36642ce6-aa3b-4aa0-bd4d-03104d1d7fa6"), "users:read");
    public static readonly Permission UsersModify = new(Guid.Parse("5f60cd71-840d-47e5-bdcc-8d21c2918240"), "users:modify");

    public static readonly Permission[] All = [UsersRead, UsersModify];

    [SetsRequiredMembers]
    public Permission(Guid id, string code)
    {
        Id = id;
        Code = code;
    }

    public string Code { get; private set; }
}
