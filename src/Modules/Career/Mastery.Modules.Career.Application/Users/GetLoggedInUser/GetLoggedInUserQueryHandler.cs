using Mastery.Modules.Career.Application.Abstractions.Messaging;
using Mastery.Modules.Career.Domain.Abstractions;

namespace Mastery.Modules.Career.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    public Task<Result<UserResponse>> Handle(GetLoggedInUserQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Implement using dapper and sql connection factory.");
    }
}
