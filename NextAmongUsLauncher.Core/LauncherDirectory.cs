using NextAmongUsLauncher.Core.Utils;

namespace NextAmongUsLauncher.Core;

public class LauncherDirectory
{
    public readonly string CURRENT_PATH = null!;

    public string CONFIG_PATH = null!;

    public string DLL_PATH = null!;

    public string LOG_PATH = null!;

    public string TEMP_PATH = null!;

    static LauncherDirectory()
    {
        Instance = new LauncherDirectory();
    }

    public LauncherDirectory()
    {
        if (Instance != null || Instance != this) return;
        Instance = this;

        CURRENT_PATH = Directory.GetCurrentDirectory();
        DLL_PATH = $"{CURRENT_PATH}/DLL";
        TEMP_PATH = $"{CURRENT_PATH}/TEMP";
        CONFIG_PATH = $"{CURRENT_PATH}/Config";
        LOG_PATH = $"{CURRENT_PATH}/Logs";

        INIT();
    }

    public static LauncherDirectory? Instance { get; private set; }

    private void INIT()
    {
        var Paths = typeof(LauncherDirectory).GetFieldValues<string>(null, this);

        foreach (var VarPath in Paths.Where(VarPath => !Directory.Exists(VarPath))) Directory.CreateDirectory(VarPath);
    }
}