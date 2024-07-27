﻿namespace Mastery.Career.Domain.Users;

public sealed record Email
{
    private Email() { }

    public string Value { get; private init; } = default!;

    public static Email Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        // todo; regex validation

        return new Email
        {
            Value = value,
        };
    }
}
