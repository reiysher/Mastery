using System.Diagnostics;
using System.Net.Mime;
using Mastery.Common.Infrastructure.ExceptionHandling.ExceptionDetails;
using Mastery.Common.Infrastructure.ExceptionHandling.StackFrames;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Mastery.Common.Infrastructure.ExceptionHandling;

internal sealed class GlobalExceptionHandler(
    ExceptionDetailsProvider detailsProvider,
    IOptions<ExceptionHandlingOptions> options)
    : IExceptionHandler
{
    private readonly ExceptionHandlingOptions _options = options.Value;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _options.TryMapProblemDetails(httpContext, exception, out ProblemDetails? problemDetails);

        problemDetails ??= new ProblemDetails();
        problemDetails.Title = "Internal Server Error";
        problemDetails.Status ??= StatusCodes.Status500InternalServerError;
        problemDetails.Type ??= "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";

        problemDetails.Extensions.TryAdd(
            _options.TraceIdPropertyName!,
            Activity.Current?.Id ?? httpContext.TraceIdentifier);

        if (_options.IncludeExceptionDetails!(httpContext, exception))
        {
            problemDetails.Title ??= TypeNameHelper.GetTypeDisplayName(exception.GetType());
            problemDetails.Instance ??= GetHelpLink(exception);
            problemDetails.Detail ??= exception.Message;
            problemDetails.Extensions.TryAdd(
                _options.ExceptionDetailsPropertyName!,
                GetErrors(detailsProvider.GetDetails(exception)).ToArray());
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static IEnumerable<ErrorDetails> GetErrors(IEnumerable<ExceptionDetails.ExceptionDetails> details)
    {
        foreach (ExceptionDetails.ExceptionDetails detail in details)
        {
            yield return new ErrorDetails(detail);
        }
    }

    private static string? GetHelpLink(Exception exception)
    {
        string? link = exception.HelpLink;

        if (string.IsNullOrEmpty(link))
        {
            return null;
        }

        if (Uri.TryCreate(link, UriKind.Absolute, out Uri? result))
        {
            return result.ToString();
        }

        return null;
    }
}

public sealed class ErrorDetails(ExceptionDetails.ExceptionDetails detail)
{
    public string? Message { get; } = detail.Error?.Message;

    public string? Type { get; } = TypeNameHelper.GetTypeDisplayName(detail.Error);

    public string? Raw { get; } = detail.Error?.ToString();

    public IReadOnlyCollection<StackFrame> StackFrames { get; } = GetStackFrames(detail.StackFrames).ToList();

    private static IEnumerable<StackFrame> GetStackFrames(IEnumerable<StackFrameSourceCodeInfo> stackFrames)
    {
        foreach (StackFrameSourceCodeInfo stackFrame in stackFrames)
        {
            yield return new StackFrame
            {
                FilePath = stackFrame.File,
                FileName = string.IsNullOrEmpty(stackFrame.File) ? null : Path.GetFileName(stackFrame.File),
                Function = stackFrame.Function,
                Line = GetLineNumber(stackFrame.Line),
                PreContextLine = GetLineNumber(stackFrame.PreContextLine),
                PreContextCode = GetCode(stackFrame.PreContextCode),
                ContextCode = GetCode(stackFrame.ContextCode),
                PostContextCode = GetCode(stackFrame.PostContextCode),
            };
        }
    }

    private static int? GetLineNumber(int lineNumber)
    {
        if (lineNumber == 0)
        {
            return null;
        }

        return lineNumber;
    }

    private static IReadOnlyCollection<string>? GetCode(IEnumerable<string> code)
    {
        var list = code.ToList();
        return list.Count > 0 ? list : null;
    }

    public class StackFrame
    {
        public string? FilePath { get; set; }

        public string? FileName { get; set; }

        public string? Function { get; set; }

        public int? Line { get; set; }

        public int? PreContextLine { get; set; }

        public IReadOnlyCollection<string>? PreContextCode { get; set; }

        public IReadOnlyCollection<string>? ContextCode { get; set; }

        public IReadOnlyCollection<string>? PostContextCode { get; set; }
    }
}
