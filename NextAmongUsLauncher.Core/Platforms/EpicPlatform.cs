using System.Diagnostics;
using NextAmongUsLauncher.Core.Base;

namespace NextAmongUsLauncher.Core.Platforms;

public class EpicPlatform(string gameExePath) : Platform(gameExePath)
{
    public override GamePlatform GamePlatform => GamePlatform.Epic;
    
    public override Process? GameProcess { get; protected set; }


}