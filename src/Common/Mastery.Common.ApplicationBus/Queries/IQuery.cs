using MediatR;

namespace Mastery.Common.ApplicationBus.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>;
