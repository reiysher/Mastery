namespace Mastery.Modules.Career.Domain.SharedKernel;

public static class NoteErrors
{
    public static readonly Error InvalidValue = new("Note.Value", "Provided value is invalid");
}
