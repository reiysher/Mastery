namespace Mastery.Common.Application.Exceptions;

public sealed class MasteryException : Exception
{
    public MasteryException(string requestName, string? error = null, Exception? innerException = default)
        : base("Application Exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public string? Error { get; }
}
