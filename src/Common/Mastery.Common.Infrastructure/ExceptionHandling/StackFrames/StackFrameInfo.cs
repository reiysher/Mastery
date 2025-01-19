namespace Mastery.Common.Infrastructure.ExceptionHandling.StackFrames;

internal class StackFrameInfo(
    int lineNumber,
    string? filePath,
    System.Diagnostics.StackFrame? stackFrame,
    MethodDisplayInfo? methodDisplayInfo)
{
    public int LineNumber { get; } = lineNumber;

    public string? FilePath { get; } = filePath;

    public System.Diagnostics.StackFrame? StackFrame { get; } = stackFrame;

    public MethodDisplayInfo? MethodDisplayInfo { get; } = methodDisplayInfo;
}
