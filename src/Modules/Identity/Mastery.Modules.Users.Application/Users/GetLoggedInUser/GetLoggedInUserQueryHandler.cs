using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;

namespace Mastery.Modules.Users.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    public Task<Result<UserResponse>> Handle(GetLoggedInUserQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Implement using dapper and sql connection factory.");
    }
}
