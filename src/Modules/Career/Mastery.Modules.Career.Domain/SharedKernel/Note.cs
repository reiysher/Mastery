namespace Mastery.Modules.Career.Domain.SharedKernel;

public sealed record Note
{
    public string Value { get; private init; }

    private Note(string value)
    {
        Value = value;
    }

    public static Result<Note> New(string? value)
    {
        if(value == null)
        {
            return Result.Failure<Note>(NoteErrors.InvalidValue);
        }

        return new Note(value);
    }
}
