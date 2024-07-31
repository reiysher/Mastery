﻿using Mastery.Common.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mastery.Common.Application.Behaviors;

internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception for {RequestName}", typeof(TRequest).Name);

            throw new MasteryException(typeof(TRequest).Name, innerException: ex);
        }
    }
}
