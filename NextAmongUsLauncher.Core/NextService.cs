using Microsoft.Extensions.DependencyInjection;

namespace NextAmongUsLauncher.Core;

public class NextService
{
    private static NextService? Instance { get; set; }
    
    public static NextService GetInstance(bool Build = false, List<Type>? types = null) {
        if (Instance != null)
        {
            return Instance;
        }
        
        Instance = new NextService();

        if (types != null)
            Instance.BuildList = types;
        
        if (Build)
            Instance.Build();
        
        return Instance;
    }
    
    private static IServiceProvider _serviceProvider = null!;

    private static bool BuildComplete;

    private List<Type> BuildList = new ();

    public IServiceCollection? _FastService { get; private set; }

    public HashSet<IServiceProvider> ServiceProviders = new();

    private NextService()
    {
        _serviceProvider = null!;
        _FastService = null;
        BuildComplete = false;
    }

    public void Add(params Type[] types)
    {
        if (BuildComplete) return;
        BuildList.AddRange(types);
    }

    public void Fast()
    {
        Destroy();
        _FastService = new ServiceCollection();
    }
    
    public void Build()
    {
        if (BuildComplete) return;

        try
        {

            if (_FastService != null)
            {
                _serviceProvider = _FastService.BuildServiceProvider();
                BuildComplete = true;
            }
            
            var Service = new ServiceCollection();

            if (BuildList.Count != 0)
            {
                foreach (var VarType in BuildList)
                {
                    Service.AddSingleton(VarType);
                }
            }
            
            // 默认添加
            {
                Service.AddSingleton<HttpClient>();
            }
            
            _serviceProvider = Service.BuildServiceProvider();
            BuildComplete = true;
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }

    public void Destroy()
    {
        _serviceProvider = null!;
        _FastService = null;
        BuildComplete = false;
        Instance = null;
    }

    public T? GetService<T>() =>
        _serviceProvider.GetService<T>();

}