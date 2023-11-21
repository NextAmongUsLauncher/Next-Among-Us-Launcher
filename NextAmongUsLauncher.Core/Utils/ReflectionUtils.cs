using System.Reflection;

namespace NextAmongUsLauncher.Core.Utils;

public static class ReflectionUtils
{
    public static List<T> GetFieldValues<T>(this Type type, BindingFlags? flags, object? Instance)
    {
        var Fields = flags is null ? type.GetFields() : type.GetFields((BindingFlags)flags);
        var Values = Fields.Select(n => n.GetValue(Instance));
        return Values.Cast<T>().ToList();
    }
}