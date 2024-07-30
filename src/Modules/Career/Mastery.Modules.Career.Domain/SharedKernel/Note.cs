namespace Mastery.Modules.Career.Domain.SharedKernel;

public sealed record Note
{
    public string Value { get; private init; }

    private Note(string value)
    {
        Value = value;
    }

    public static Note New(string? value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new Note(value);
    }
}
