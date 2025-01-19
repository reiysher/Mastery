using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mastery.Common.Infrastructure.ExceptionHandling;

public sealed class ExceptionHandlingOptions
{
    public Func<HttpContext, Exception, bool>? IncludeExceptionDetails { get; set; }

    public string? TraceIdPropertyName { get; set; }

    public string? ExceptionDetailsPropertyName { get; set; }

    private List<ExceptionMapper> Mappers { get; } = [];

    public void MapToStatusCode<TException>(int statusCode) where TException : Exception
    {
        Map<TException>((_, _) => new ProblemDetails() { Status = statusCode });
    }

    public void Map<TException>(Func<HttpContext, TException, ProblemDetails?> mapping) where TException : Exception
    {
        Map((_, _) => true, mapping);
    }

    public void Ignore<TException>() where TException : Exception
    {
        Map<TException>((_, _) => null);
    }

    public void Map<TException>(
        Func<HttpContext, TException, bool> predicate,
        Func<HttpContext, TException, ProblemDetails?> mapping)
        where TException : Exception
    {
        Mappers.Add(new ExceptionMapper(
            typeof(TException),
            (ctx, ex) => mapping(ctx, (TException)ex),
            (ctx, ex) => predicate(ctx, (TException)ex)));
    }

    internal bool TryMapProblemDetails(HttpContext context, Exception? exception, out ProblemDetails? problem)
    {
        if (exception is null)
        {
            problem = default;
            return false;
        }

        foreach (ExceptionMapper mapper in Mappers)
        {
            if (mapper.TryMap(context, exception, out problem))
            {
                return true;
            }
        }

        problem = default;
        return false;
    }

    private sealed class ExceptionMapper
    {
        public ExceptionMapper(Type type, Func<HttpContext, Exception, ProblemDetails?> mapping,
            Func<HttpContext, Exception, bool> predicate)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        private Type Type { get; }

        private Func<HttpContext, Exception, ProblemDetails?> Mapping { get; }

        private Func<HttpContext, Exception, bool> Predicate { get; }

        public bool ShouldMap(HttpContext context, Exception exception)
        {
            return Type.IsInstanceOfType(exception) && Predicate(context, exception);
        }

        public bool TryMap(HttpContext context, Exception exception, out ProblemDetails? problem)
        {
            if (ShouldMap(context, exception))
            {
                try
                {
                    problem = Mapping(context, exception);
                    return true;
                }
                catch
                {
                    problem = default;
                    return false;
                }
            }

            problem = default;
            return false;
        }
    }
}
