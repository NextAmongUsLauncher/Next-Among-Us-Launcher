using System.Collections;
using System.Collections.ObjectModel;
using NextAmongUsLauncher.Core.NextConsole.Logs;

namespace NextAmongUsLauncher.Core.NextConsole;

public class Logger
{
    public static Logger Instance { get; } = new();
    
    public ObservableCollection<ILogListener> Listeners { get; private set; }
    
    public SourceCollection Sources { get; private set; }

    static Logger()
    {
        
    }

    public Logger()
    {
        Listeners = new ObservableCollection<ILogListener>();
        Sources = new SourceCollection(Listeners);
    }

    public void RegisterListener(ILogListener listener) =>
        Listeners.Add(listener);

    public void UnregisterListener(ILogListener listener)
    {
        listener.Dispose();
        Listeners.Remove(listener);
    }

    public void ClearListener()
    {
        foreach (var logListener in Listeners)
        {
            logListener.Dispose();
        }
        
        Listeners.Clear();
    }
}

public class SourceCollection : List<ILogSource>, ICollection<ILogSource>
{
    public ObservableCollection<ILogListener> Listeners;

    bool ICollection<ILogSource>.IsReadOnly => false;
    
    void ICollection<ILogSource>.Add(ILogSource item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        
        
        base.Add(item);
    }

    public SourceCollection(ObservableCollection<ILogListener> logListeners)
    {
        Listeners = logListeners;
    }

    public void SetListener(ObservableCollection<ILogListener> listeners)
    {
        Listeners = listeners;
    }
}