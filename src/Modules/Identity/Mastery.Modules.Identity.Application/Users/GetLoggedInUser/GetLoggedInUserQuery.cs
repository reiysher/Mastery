using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Identity.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
