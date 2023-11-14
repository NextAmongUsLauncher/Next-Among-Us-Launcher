namespace NextAmongUsLauncher.Core.NextConsole.Logs;

public abstract class LogEventArgs : EventArgs
{
    public LogLevel LogLevel { get; }
    
    public ILogSource  Source { get; }
    
    public object Data { get; }
    
    public LogEventArgs(LogLevel logLevel, ILogSource source, object data)
    {
        LogLevel = logLevel;
        
        Source = source;
        
        Data = data;
    }
    
    
}