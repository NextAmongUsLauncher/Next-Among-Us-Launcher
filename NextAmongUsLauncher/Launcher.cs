using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NextAmongUsLauncher.Core.NextConsole;
using NextAmongUsLauncher.Core.Utils;

namespace NextAmongUsLauncher;

public sealed class Launcher
{
    /// <summary>
    /// 启动器实例
    /// </summary>
    public static Launcher Instance { get; private set; }
    
    /// <summary>
    /// 是否为开发模式
    /// </summary>
    public static bool IsDev { get; private set; } = false;
    
    /// <summary>
    /// 主窗口
    /// </summary>
    public readonly Window MainWindow;
    
    /// <summary>
    /// 管理启动器控制台
    /// </summary>
    public ConsoleManager ConsoleManager { get; private set; }
    
    /// <summary>
    ///  所有窗口
    /// </summary>
    public HashSet<Window> AllWindow { get; private set; } = new();
    
    /// <summary>
    ///  所有页面
    /// </summary>
    public HashSet<Page> AllPage { get; private set; } = new();
    
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
#if DEBUG
        SetDev(true);
#endif
        
        ConsoleManager = new ConsoleManager("Next Among Us Launcher");
        
        if (IsDev)
        {
            ConsoleManager.CreateConsole();
        }
    }

    public void Close()
    {
        
    }

    internal void SetDev(bool dev) => IsDev = dev;
}