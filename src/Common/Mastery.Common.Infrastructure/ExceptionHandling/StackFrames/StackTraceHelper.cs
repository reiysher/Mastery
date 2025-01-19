using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Mastery.Common.Infrastructure.ExceptionHandling.StackFrames;

internal sealed class StackTraceHelper
{
    public static IList<StackFrameInfo> GetFrames(Exception exception, out AggregateException? error)
    {
        const bool needFileInfo = true;
        var stackTrace = new StackTrace(exception, needFileInfo);
        StackFrame[] stackFrames = stackTrace.GetFrames();

        var frames = new List<StackFrameInfo>(stackFrames.Length);

        for (int i = 0; i < stackFrames.Length; i++)
        {
            StackFrame frame = stackFrames[i];
            MethodBase? method = frame.GetMethod();

            if (!ShowInStackTrace(method) && i < stackFrames.Length - 1)
            {
                continue;
            }

            var stackFrame = new StackFrameInfo(frame.GetFileLineNumber(), frame.GetFileName(), frame, GetMethodDisplayString(frame.GetMethod()));
            frames.Add(stackFrame);
        }

        error = null;
        return frames;
    }

    private static MethodDisplayInfo? GetMethodDisplayString(MethodBase? method)
    {
        if (method == null)
        {
            return null;
        }

        Type? type = method.DeclaringType;

        string methodName = method.Name;

        string? subMethod = null;
        if (type != null && type.IsDefined(typeof(CompilerGeneratedAttribute)) &&
            (typeof(IAsyncStateMachine).IsAssignableFrom(type) || typeof(IEnumerator).IsAssignableFrom(type)))
        {
            if (TryResolveStateMachineMethod(ref method, out type))
            {
                subMethod = methodName;
            }
        }

        string? declaringTypeName = null;
        if (type != null)
        {
            declaringTypeName = TypeNameHelper.GetTypeDisplayName(type, includeGenericParameterNames: true);
        }

        string? genericArguments = null;
        if (method.IsGenericMethod)
        {
            genericArguments = "<" + string.Join(", ", method.GetGenericArguments()
                .Select(arg => TypeNameHelper.GetTypeDisplayName(arg, fullName: false, includeGenericParameterNames: true))) + ">";
        }

        IEnumerable<ParameterDisplayInfo> parameters = method.GetParameters().Select(parameter =>
        {
            Type parameterType = parameter.ParameterType;

            string prefix = string.Empty;
            if (parameter.IsOut)
            {
                prefix = "out";
            }
            else if (parameterType.IsByRef)
            {
                prefix = "ref";
            }

            if (parameterType.IsByRef)
            {
                parameterType = parameterType.GetElementType();
            }

            string parameterTypeString = TypeNameHelper.GetTypeDisplayName(parameterType!, fullName: false, includeGenericParameterNames: true);

            return new ParameterDisplayInfo
            {
                Prefix = prefix,
                Name = parameter.Name,
                Type = parameterTypeString,
            };
        });

        var methodDisplayInfo = new MethodDisplayInfo(declaringTypeName, method.Name, genericArguments, subMethod, parameters);

        return methodDisplayInfo;
    }

    private static bool ShowInStackTrace(MethodBase? method)
    {
        Debug.Assert(method != null);

        if (HasStackTraceHiddenAttribute(method))
        {
            return false;
        }

        Type? type = method.DeclaringType;
        if (type == null)
        {
            return true;
        }

        if (HasStackTraceHiddenAttribute(type))
        {
            return false;
        }

        if (type == typeof(ExceptionDispatchInfo) && method.Name == "Throw")
        {
            return false;
        }
        else if (type == typeof(TaskAwaiter) ||
                 type == typeof(TaskAwaiter<>) ||
                 type == typeof(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter) ||
                 type == typeof(ConfiguredTaskAwaitable<>.ConfiguredTaskAwaiter))
        {
            switch (method.Name)
            {
                case "HandleNonSuccessAndDebuggerNotification":
                case "ThrowForNonSuccess":
                case "ValidateEnd":
                case "GetResult":
                    return false;
            }
        }

        return true;
    }

    private static bool TryResolveStateMachineMethod(ref MethodBase method, out Type? declaringType)
    {
        Debug.Assert(method != null);
        Debug.Assert(method.DeclaringType != null);

        declaringType = method.DeclaringType;

        Type? parentType = declaringType.DeclaringType;
        if (parentType == null)
        {
            return false;
        }

        MethodInfo[] methods = parentType.GetMethods(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Static |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly);

        foreach (MethodInfo candidateMethod in methods)
        {
            IEnumerable<StateMachineAttribute> attributes = candidateMethod.GetCustomAttributes<StateMachineAttribute>();

            foreach (StateMachineAttribute asma in attributes)
            {
                if (asma.StateMachineType == declaringType)
                {
                    method = candidateMethod;
                    declaringType = candidateMethod.DeclaringType;
                    return asma is IteratorStateMachineAttribute;
                }
            }
        }

        return false;
    }

    private static bool HasStackTraceHiddenAttribute(MemberInfo memberInfo)
    {
        IList<CustomAttributeData> attributes;
        try
        {
            attributes = memberInfo.GetCustomAttributesData();
        }
        catch
        {
            return false;
        }

        return attributes.Any(t => t.AttributeType.Name == "StackTraceHiddenAttribute");
    }
}
