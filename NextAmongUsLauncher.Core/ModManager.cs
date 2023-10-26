namespace NextAmongUsLauncher.Core;

public class ModManager : GetBase<ModManager>
{
    public ModManager()
    {
    }
    
    public List<Mod> AllMod { get; private set; } = new();
    private Mod? CurrentMod;

    public Mod GetCurrentMod()
    {
        if (CurrentMod != default ) return CurrentMod;
        var mod = new Mod
        {
            Name = "None",
            Version = "0.0.0",
            PUID = "mod"
        };
        return CurrentMod = mod;
    }
}