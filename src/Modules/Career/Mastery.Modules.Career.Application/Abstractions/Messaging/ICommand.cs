using Mastery.Modules.Career.Domain.Abstractions;
using MediatR;

namespace Mastery.Modules.Career.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
