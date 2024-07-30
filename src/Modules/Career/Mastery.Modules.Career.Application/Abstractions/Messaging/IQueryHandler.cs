using Mastery.Modules.Career.Domain.Abstractions;
using MediatR;

namespace Mastery.Modules.Career.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
