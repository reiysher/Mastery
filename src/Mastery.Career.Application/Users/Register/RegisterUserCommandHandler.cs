using Mastery.Career.Application.Abstractions.Data;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;
using Mastery.Career.Domain.Users;

namespace Mastery.Career.Application.Users.Register;

internal sealed class RegisterUserCommandHandler(
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IAuthenticationService authenticationService = authenticationService;
    private readonly IUserRepository userRepository = userRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(command.FirstName, command.LastName, command.Email);

        string identityId = await authenticationService.RegisterAsync(
            user,
            command.Password,
            cancellationToken);

        user.SetIdentityId(identityId);

        userRepository.Add(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
