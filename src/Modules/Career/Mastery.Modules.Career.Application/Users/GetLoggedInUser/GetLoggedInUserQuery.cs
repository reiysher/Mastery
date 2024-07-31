using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Career.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
