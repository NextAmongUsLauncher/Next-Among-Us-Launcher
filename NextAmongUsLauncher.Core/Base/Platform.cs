using System.Diagnostics;
using NextAmongUsLauncher.Core.Platforms;

namespace NextAmongUsLauncher.Core.Base;

public abstract class Platform(string gameExePath)
{
    public readonly string GameExePath = gameExePath;

    public string? GameDirectory = Path.GetDirectoryName(gameExePath);

    public string? BepInExDirectory;
    
    public string? BepInExPluginsDirectory;
    
    public string? BepInExConfigDirectory;

    public abstract GamePlatform GamePlatform { get; }
    
    public abstract Process? GameProcess { get; protected set; }

    public virtual void StartGame()
    {
        GameProcess = Process.Start(GameExePath);
    }

    public virtual void DownloadGame()
    {
    }

    public virtual void RemoveGame()
    {
        
    }

    public virtual void QuitGame()
    {
        var game = Process.GetProcesses().FirstOrDefault(n => n.ProcessName == "Among Us.exe");
        game?.Kill();
    }

    public virtual PlatformIntegrity CheckIntegrity()
    {
        return PlatformIntegrity.None;
    }

    public static Platform? GetPlatform(string path)
    {
        if (!File.Exists(path))
            return null;
        
        Console.WriteLine(path);
        var fileInfo = new FileInfo(path);
        var directory = fileInfo.Directory;
        
        if (directory == null) return null;
        
        var Directories = directory.GetDirectories();
        
        if (Directories.Any(n => n.Name == ".egstore"))
        {
            return new EpicPlatform(path);
        }

        return new SteamPlatform(path);
    }
}

[Flags]
public enum PlatformIntegrity
{
    None,
    Integrity,
    Deficiency
}

public enum GamePlatform
{
    None,
    Epic,
    Steam
}