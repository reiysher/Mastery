using Microsoft.Extensions.Logging;

namespace Mastery.Common.Infrastructure.ExceptionHandling.ExceptionDetails;

internal static class LoggerExtensions
{
    private static readonly Action<ILogger, Exception> FailedToReadStackFrameInfo;

    static LoggerExtensions()
    {
        FailedToReadStackFrameInfo = LoggerMessage.Define(
            logLevel: LogLevel.Debug,
            eventId: new EventId(0, "FailedToReadStackTraceInfo"),
            formatString: "Failed to read stack trace information for exception.");
    }

    public static void FailedToReadStackTraceInfo(this ILogger logger, Exception exception)
    {
        FailedToReadStackFrameInfo(logger, exception);
    }
}
