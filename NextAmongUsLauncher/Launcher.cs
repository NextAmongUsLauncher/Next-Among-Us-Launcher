using System.Collections.Generic;
using Microsoft.UI.Xaml;

namespace NextAmongUsLauncher;

public sealed class Launcher
{
    public static Launcher Instance { get; private set; }
    public static bool IsDev { get; private set; } = false;
    
    public readonly Window MainWindow;
    public HashSet<Window> AllWindow { get; private set; } = new();
    
    public Launcher(Window Window)
    {
        Instance = this;
        MainWindow = Window;
        MainWindow.Activate();
        AllWindow.Add(MainWindow);
        Start();
    }

    internal void Start()
    {
        
    }

    public void Close()
    {
        
    }
}