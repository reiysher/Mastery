using Mastery.Common.Domain;
using MediatR;

namespace Mastery.Common.Application.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;
