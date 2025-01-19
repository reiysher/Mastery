using System.Text;

namespace Mastery.Common.Infrastructure.ExceptionHandling.StackFrames;

internal sealed class ParameterDisplayInfo
{
    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Prefix { get; set; }

    public override string ToString()
    {
        var builder = new StringBuilder();
        if (!string.IsNullOrEmpty(Prefix))
        {
            builder
                .Append(Prefix)
                .Append(' ');
        }

        builder.Append(Type);
        builder.Append(' ');
        builder.Append(Name);

        return builder.ToString();
    }
}
