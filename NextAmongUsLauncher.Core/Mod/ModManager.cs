namespace NextAmongUsLauncher.Core;

public class ModManager : GetBase<ModManager>
{
    public ModManager()
    {
    }
    
    public List<Mod> AllMod { get; private set; } = new();
    private Mod? CurrentMod;

    public void LoadMods(DirectoryInfo directory)
    {
        
    }

    public void ClearMods()
    {
        
    }

    public Mod GetCurrentMod()
    {
        if (CurrentMod != default ) return CurrentMod;
        var mod = new Mod
        {
            Name = "None",
            Version = "0.0.0",
            PUID = "mod",
            IsDefault = true
        };
        return CurrentMod = mod;
    }
}