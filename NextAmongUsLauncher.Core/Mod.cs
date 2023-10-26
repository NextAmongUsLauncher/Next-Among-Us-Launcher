using System.Reflection;

namespace NextAmongUsLauncher.Core;

public sealed class Mod
{
    public string PUID;
    public string Name;
    public string Version;
    public string Author;
    public string Description;
    public Assembly ModInstance;
    public object ModBaseClass;
    
    
    public Mod()
    {
        
    }
    
    
}