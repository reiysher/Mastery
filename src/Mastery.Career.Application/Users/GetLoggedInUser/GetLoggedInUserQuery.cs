using Mastery.Career.Application.Abstractions.Messaging;

namespace Mastery.Career.Application.Users.GetLoggedInUser;

public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
