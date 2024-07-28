namespace Mastery.Career.Application.Exceptions;

public sealed class ConcurrencyException(string message, Exception ineerException)
    : Exception(message, ineerException);
