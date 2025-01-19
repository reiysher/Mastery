using System.Reflection;
using Mastery.Common.Infrastructure.ExceptionHandling.StackFrames;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mastery.Common.Infrastructure.ExceptionHandling.ExceptionDetails;

public sealed class ExceptionDetailsProvider(
    IHostEnvironment environment,
    ILogger<ExceptionDetailsProvider> logger)
{
    private const int SourceCodeLineCount = 6;

    private readonly IFileProvider _fileProvider = environment.ContentRootFileProvider;

    public IEnumerable<ExceptionDetails> GetDetails(Exception exception)
    {
        IEnumerable<Exception> exceptions = FlattenAndReverseExceptionTree(exception);

        foreach (Exception ex in exceptions)
        {
            yield return new ExceptionDetails(ex, GetStackFrames(ex));
        }
    }

    private IEnumerable<StackFrameSourceCodeInfo> GetStackFrames(Exception original)
    {
        IEnumerable<StackFrameSourceCodeInfo> stackFrames = StackTraceHelper.GetFrames(original, out AggregateException? exception)
            .Select(frame => GetStackFrameSourceCodeInfo(
                frame.MethodDisplayInfo?.ToString(),
                frame.FilePath,
                frame.LineNumber));

        if (exception != null)
        {
            logger.FailedToReadStackTraceInfo(exception);
        }

        return stackFrames;
    }

    private static List<Exception> FlattenAndReverseExceptionTree(Exception? ex)
    {
        if (ex is ReflectionTypeLoadException typeLoadException)
        {
            var typeLoadExceptions = new List<Exception>();
            foreach (Exception? loadException in typeLoadException.LoaderExceptions)
            {
                typeLoadExceptions.AddRange(FlattenAndReverseExceptionTree(loadException));
            }

            typeLoadExceptions.Add(typeLoadException);
            return typeLoadExceptions;
        }

        var list = new List<Exception>();
        if (ex is AggregateException aggregateException)
        {
            list.Add(ex);
            foreach (Exception innerException in aggregateException.Flatten().InnerExceptions)
            {
                list.Add(innerException);
            }
        }

        else
        {
            while (ex != null)
            {
                list.Add(ex);
                ex = ex.InnerException;
            }
            list.Reverse();
        }

        return list;
    }

    private StackFrameSourceCodeInfo GetStackFrameSourceCodeInfo(
        string? method,
        string? filePath,
        int lineNumber)
    {
        var stackFrame = new StackFrameSourceCodeInfo
        {
            Function = method,
            File = filePath,
            Line = lineNumber
        };

        if (string.IsNullOrEmpty(stackFrame.File))
        {
            return stackFrame;
        }

        IEnumerable<string>? lines = null;
        if (File.Exists(stackFrame.File))
        {
            lines = File.ReadLines(stackFrame.File);
        }
        else
        {
            IFileInfo fileInfo = _fileProvider.GetFileInfo(stackFrame.File);
            if (fileInfo.Exists)
            {
                lines = !string.IsNullOrEmpty(fileInfo.PhysicalPath)
                    ? File.ReadLines(fileInfo.PhysicalPath)
                    : ReadLines(fileInfo);
            }
        }

        if (lines != null)
        {
            ReadFrameContent(stackFrame, lines, stackFrame.Line, stackFrame.Line);
        }

        return stackFrame;
    }

    private void ReadFrameContent(
        StackFrameSourceCodeInfo frame,
        IEnumerable<string> allLines,
        int errorStartLineNumberInFile,
        int errorEndLineNumberInFile)
    {
        int preErrorLineNumberInFile = Math.Max(errorStartLineNumberInFile - SourceCodeLineCount, 1);
        int postErrorLineNumberInFile = errorEndLineNumberInFile + SourceCodeLineCount;
        string[] codeBlock = allLines
            .Skip(preErrorLineNumberInFile - 1)
            .Take(postErrorLineNumberInFile - preErrorLineNumberInFile + 1)
            .ToArray();

        int numOfErrorLines = errorEndLineNumberInFile - errorStartLineNumberInFile + 1;
        int errorStartLineNumberInArray = errorStartLineNumberInFile - preErrorLineNumberInFile;

        frame.PreContextLine = preErrorLineNumberInFile;
        frame.PreContextCode = codeBlock.Take(errorStartLineNumberInArray).ToArray();
        frame.ContextCode = codeBlock
            .Skip(errorStartLineNumberInArray)
            .Take(numOfErrorLines)
            .ToArray();
        frame.PostContextCode = codeBlock
            .Skip(errorStartLineNumberInArray + numOfErrorLines)
            .ToArray();
    }

    private static IEnumerable<string> ReadLines(IFileInfo fileInfo)
    {
        using var reader = new StreamReader(fileInfo.CreateReadStream());
        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }
}
