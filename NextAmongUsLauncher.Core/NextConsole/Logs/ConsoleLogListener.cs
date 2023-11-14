namespace NextAmongUsLauncher.Core.NextConsole.Logs;

public class ConsoleLogListener : ILogListener
{
    public ConsoleLogListener(ConsoleManager manager)
    {
        LogOut = manager.ConsoleOut;
    }

    public ConsoleLogListener(TextWriter logOut)
    {
        LogOut = logOut;
    }
    
    public void Dispose()
    {
    }
    
    public LogLevel ActiveLevels { get; set; }

    public LogLevel CurrentLevel { get; set; }
    
    public TextWriter? LogOut { get; set; }
    
    public void Log(string message, LogLevel logLevel)
    {
    }
}