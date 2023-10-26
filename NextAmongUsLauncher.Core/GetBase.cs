namespace NextAmongUsLauncher.Core;

public class GetBase<T> where T : new()
{
    private static T? Instance;
    public static T Get() => Instance ??= new T();
}