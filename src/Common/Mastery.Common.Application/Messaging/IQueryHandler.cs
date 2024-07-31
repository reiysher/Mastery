using Mastery.Common.Domain;
using MediatR;

namespace Mastery.Common.Application.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
