namespace NextAmongUsLauncher.Core;

public abstract class PluginBase
{
    public string Author;
    public string Description;
    public string Icon;
    public string License;
    public string Name;
    public string PUID;
    public string URL;
    public string Version;

    public abstract void Load();

    public virtual void Unload()
    {
    }
}