using Mastery.Modules.Career.Domain.Abstractions;

namespace Mastery.Modules.Career.Application.Exceptions;

public sealed class CareerException : Exception
{
    public CareerException(string requestName, Error? error, Exception? innerException = default)
        : base("Application Exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
