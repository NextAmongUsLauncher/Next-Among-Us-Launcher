namespace NextAmongUsLauncher.Core;

public abstract class PluginBase
{
    public string Name;
    public string Version;
    public string Author;
    public string PUID;
    public string Description;
    public string Icon;
    public string URL;
    public string License;
    
    public abstract void Load();
    public virtual void Unload() {}
}