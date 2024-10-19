using Mastery.Common.Application.Messaging;
using Mastery.Common.Application.Security;
using Mastery.Modules.Identity.Domain.Users;

namespace Mastery.Modules.Identity.Application.Identity.GetCurrentUser;

internal sealed class GetCurrentUserHandler(
    IUserRepository userRepository,
    ICurrentUserContext currentUserContext)
    : IQueryHandler<GetCurrentUserQuery, GetCurrentUserResponse>
{
    public async Task<GetCurrentUserResponse> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
    {
        GetCurrentUserResponse? userDto = await userRepository.GetByIdAsync(
            currentUserContext.UserId,
            user => new GetCurrentUserResponse(
                user.Name.FirstName,
                user.PhoneNumber.CountryCode + user.PhoneNumber.Value),
            cancellationToken);

        if (userDto == null)
        {
            throw new InvalidOperationException();
        }

        return userDto;
    }
}
