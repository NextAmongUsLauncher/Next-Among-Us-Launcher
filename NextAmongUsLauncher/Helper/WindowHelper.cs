using System.Linq;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace NextAmongUsLauncher.Helper;

public static class WindowHelper
{
    public static T OpenWindow<T>() where T : Window, new()
    {
        if (Instance.AllWindow.Any(n => n is T))
        {
            var window = Instance.AllWindow.FirstOrDefault(n => n is T) as T;
            WindowTop(window);
            return window;
        }
        
        var NewWindow = new T();
        NewWindow.Activate();

        return NewWindow;
    }

    public static void WindowTop(this Window window)
    {
        if (window.AppWindow == null)
            window.Activate();

        if (!window.Visible)
            window.AppWindow?.Show();
        
        window.AppWindow?.MoveInZOrderAtTop();
    }
}

public class NextWindow : Window
{
    public bool FastCreate { get; protected set; } = false;
    
    public NextWindow()
    {
        Instance.AllWindow.Add(this);
        Closed += OnClose;
        AppWindow.Destroying += OnDestroy;
        if (FastCreate)
            Activate();
    }

    public virtual void OnClose(object sender, WindowEventArgs eventArgs)
    {
        
    }

    public virtual void OnDestroy(AppWindow sender, object args)
    {
        
    }
}