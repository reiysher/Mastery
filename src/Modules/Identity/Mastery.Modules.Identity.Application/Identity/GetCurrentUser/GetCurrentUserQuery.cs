using Mastery.Common.Application.Messaging;

namespace Mastery.Modules.Identity.Application.Identity.GetCurrentUser;

public sealed record GetCurrentUserQuery() : IQuery<GetCurrentUserResponse>;
