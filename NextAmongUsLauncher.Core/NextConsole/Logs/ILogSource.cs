namespace NextAmongUsLauncher.Core.NextConsole.Logs;

public interface ILogSource : IDisposable
{
    public string Name { get; }

    public Logger _Logger { get; }

    public EventHandler<LogEventArgs> LogEvent { get; set; }
}