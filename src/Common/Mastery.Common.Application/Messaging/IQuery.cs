using MediatR;

namespace Mastery.Common.Application.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;
