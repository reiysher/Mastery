namespace Mastery.Common.Infrastructure.ExceptionHandling.StackFrames;

public sealed class StackFrameSourceCodeInfo
{
    public string? Function { get; set; }

    public string? File { get; set; }

    public int Line { get; set; }

    public int PreContextLine { get; set; }

    public IEnumerable<string> PreContextCode { get; set; } = [];

    public IEnumerable<string> ContextCode { get; set; } = [];

    public IEnumerable<string> PostContextCode { get; set; } = [];

    public string? ErrorDetails { get; set; }
}
