using Mastery.Common.Infrastructure.ExceptionHandling.StackFrames;

namespace Mastery.Common.Infrastructure.ExceptionHandling.ExceptionDetails;

public sealed record ExceptionDetails(Exception Error, IEnumerable<StackFrameSourceCodeInfo> StackFrames);
