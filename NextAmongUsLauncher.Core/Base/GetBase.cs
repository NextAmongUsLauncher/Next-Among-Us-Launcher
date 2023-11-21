namespace NextAmongUsLauncher.Core.Base;

public class GetBase<T> where T : class, new()
{
    protected static T? Instance;
    public static T Get() => Instance ??= new T();

    public GetBase()
    {
        Instance = this as T;
    }
    
}