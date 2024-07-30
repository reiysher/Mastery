using Mastery.Modules.Career.Domain.Abstractions;
using MediatR;

namespace Mastery.Modules.Career.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
