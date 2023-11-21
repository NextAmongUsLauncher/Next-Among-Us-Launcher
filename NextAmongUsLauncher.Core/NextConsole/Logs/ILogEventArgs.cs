namespace NextAmongUsLauncher.Core.NextConsole.Logs;

public abstract class LogEventArgs(LogLevel logLevel, ILogSource source, object data) : EventArgs
{
    public LogLevel LogLevel { get; } = logLevel;

    public ILogSource  Source { get; } = source;

    public object Data { get; } = data;
}