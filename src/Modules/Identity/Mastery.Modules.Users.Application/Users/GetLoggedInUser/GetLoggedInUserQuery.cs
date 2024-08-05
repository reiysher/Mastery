using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Users.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
