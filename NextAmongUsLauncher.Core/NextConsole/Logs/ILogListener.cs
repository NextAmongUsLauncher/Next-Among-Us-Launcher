namespace NextAmongUsLauncher.Core.NextConsole.Logs;

public interface ILogListener : IDisposable
{
    public LogLevel ActiveLevels { get; set; }
    public LogLevel CurrentLevel { get; set; }

    public TextWriter? LogOut { get; set; }

    public void Log(string message, LogLevel logLevel);
}