using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;

namespace Mastery.Career.Application.Users.GetLoggedInUser;

internal sealed class GetLoggedInUserQueryHandler : IQueryHandler<GetLoggedInUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetLoggedInUserQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Implement using dapper and sql connection factory.");
    }
}
