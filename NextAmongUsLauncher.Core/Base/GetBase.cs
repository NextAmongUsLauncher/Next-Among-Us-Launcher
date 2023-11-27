namespace NextAmongUsLauncher.Core.Base;

public class GetBase<T> where T : class, new()
{
    protected static T? Instance;

    public GetBase()
    {
        Instance = this as T;
    }

    public static T Get()
    {
        return Instance ??= new T();
    }
}