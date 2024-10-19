using MediatR;

namespace Mastery.Common.Application.Messaging;

public interface ICommand : IRequest, IBaseCommand;

public interface ICommand<out TResponse> : IRequest<TResponse>, IBaseCommand;

public interface IBaseCommand;
