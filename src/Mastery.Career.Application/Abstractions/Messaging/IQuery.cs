using Mastery.Career.Domain.Abstractions;
using MediatR;

namespace Mastery.Career.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
