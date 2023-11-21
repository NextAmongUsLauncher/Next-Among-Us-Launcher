using System.Diagnostics;
using NextAmongUsLauncher.Core.Base;

namespace NextAmongUsLauncher.Core.Platforms;

public class SteamPlatform(string gameExePath) : Platform(gameExePath)
{
    public override GamePlatform GamePlatform => GamePlatform.Steam;
    
    public override Process? GameProcess { get; protected set; }

    public override void StartGame()
    {
        GameProcess = Process.Start(gameExePath);
        
    }
}