using MediatR;

namespace Mastery.Common.ApplicationBus.Commands;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>;