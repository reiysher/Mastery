using Mastery.Common.Domain;
using MediatR;

namespace Mastery.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
