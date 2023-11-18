using System.Diagnostics;

namespace NextAmongUsLauncher.Core.Base;

public abstract class Platform
{
    public abstract void StartGame();
    
    public virtual void DownloadGame()
    {}

    public virtual void DestroyGame()
    {
        
    }

    public virtual void QuitGame()
    {
        var game = Process.GetProcesses().FirstOrDefault(n => n.ProcessName == "Among Us.exe");
        game?.Kill();
    }
}