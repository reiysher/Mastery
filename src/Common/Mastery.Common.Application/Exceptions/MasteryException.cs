using Mastery.Common.Domain;

namespace Mastery.Common.Application.Exceptions;

public sealed class MasteryException : Exception
{
    public MasteryException(string requestName, Error? error, Exception? innerException = default)
        : base("Application Exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
