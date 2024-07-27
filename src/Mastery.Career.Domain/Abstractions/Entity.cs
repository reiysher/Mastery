namespace Mastery.Career.Domain.Abstractions;

public abstract class Entity<T>
    where T : notnull
{
    public required T Id { get; init; }
}
