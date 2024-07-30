﻿using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Application.Abstractions.Messaging;
using Mastery.Modules.Career.Domain.Abstractions;
using Mastery.Modules.Career.Domain.Users;

namespace Mastery.Modules.Career.Application.Users.Register;

internal sealed class RegisterUserCommandHandler(
    IAuthenticationService authenticationService,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(command.FirstName, command.LastName, command.Email);

        string identityId = await authenticationService.RegisterAsync(
            user,
            command.Password,
            cancellationToken);

        user.SetIdentityId(identityId);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
