using System.Text;

namespace Mastery.Common.Infrastructure.ExceptionHandling.StackFrames;

internal sealed class MethodDisplayInfo(
    string? declaringTypeName,
    string name,
    string? genericArguments,
    string? subMethod,
    IEnumerable<ParameterDisplayInfo> parameters)
{
    public string? DeclaringTypeName { get; } = declaringTypeName;

    public string Name { get; } = name;

    public string? GenericArguments { get; } = genericArguments;

    public string? SubMethod { get; } = subMethod;

    public IEnumerable<ParameterDisplayInfo> Parameters { get; } = parameters;

    public override string ToString()
    {
        var builder = new StringBuilder();
        if (!string.IsNullOrEmpty(DeclaringTypeName))
        {
            builder
                .Append(DeclaringTypeName)
                .Append('.');
        }

        builder.Append(Name);
        builder.Append(GenericArguments);

        builder.Append('(');
        builder.AppendJoin(", ", Parameters.Select(p => p.ToString()));
        builder.Append(')');

        if (!string.IsNullOrEmpty(SubMethod))
        {
            builder.Append('+');
            builder.Append(SubMethod);
            builder.Append("()");
        }

        return builder.ToString();
    }
}
