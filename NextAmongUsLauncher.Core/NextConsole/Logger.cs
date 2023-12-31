global using Log = NextAmongUsLauncher.Core.NextConsole.Log;
using System.Collections.ObjectModel;
using NextAmongUsLauncher.Core.NextConsole.Logs;

namespace NextAmongUsLauncher.Core.NextConsole;

public class Logger
{
    public Logger()
    {
        Listeners = new ObservableCollection<ILogListener>();
        Sources = new SourceCollection(Listeners);
    }

    public static Logger? Instance { get; private set; }

    public ObservableCollection<ILogListener> Listeners { get; }

    public SourceCollection Sources { get; private set; }

    public static void Initialize(bool ActiveConsole, ConsoleManager? consoleManager = null)
    {
        Instance = new Logger();

        var diskLogListener = new DiskLogListener();
        Instance.RegisterListener(diskLogListener);

        if (!ActiveConsole) return;

        consoleManager ??= new ConsoleManager("Next Among Us Launcher");
        consoleManager.CreateConsole();

        var ConsoleLogListener = new ConsoleLogListener(consoleManager);
        Instance.RegisterListener(ConsoleLogListener);
    }

    public void RegisterListener(ILogListener listener)
    {
        Listeners.Add(listener);
    }

    public void UnregisterListener(ILogListener listener)
    {
        listener.Dispose();
        Listeners.Remove(listener);
    }

    public void ClearListener()
    {
        foreach (var logListener in Listeners) logListener.Dispose();

        Listeners.Clear();
    }
}

public static class Log
{
    public static void Exception(Exception exception)
    {
    }
}

public class SourceCollection(ObservableCollection<ILogListener> logListeners) : List<ILogSource>,
    ICollection<ILogSource>
{
    public ObservableCollection<ILogListener> Listeners = logListeners;

    bool ICollection<ILogSource>.IsReadOnly => false;

    void ICollection<ILogSource>.Add(ILogSource item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));


        base.Add(item);
    }

    public void SetListener(ObservableCollection<ILogListener> listeners)
    {
        Listeners = listeners;
    }
}