namespace NextAmongUsLauncher.Core.NextConsole.Logs;

public class DiskLogListener : ILogListener
{
    public DiskLogListener(string name = "LogOut.log", string path = "./")
    {
        Name = name;
        Path = path;

        var stream = File.CreateText($"./{name}");
        LogOut = stream;
        CurrentLevel = LogLevel.None;
    }

    public string Name { get; set; }

    public string Path { get; set; }

    public void Dispose()
    {
        CurrentLevel = LogLevel.None;
        LogOut = null;
    }

    public LogLevel ActiveLevels { get; set; }

    public LogLevel CurrentLevel { get; set; }

    public TextWriter? LogOut { get; set; }

    public void Log(string message, LogLevel logLevel)
    {
        CurrentLevel = logLevel;
        LogOut?.WriteLine(message);
        LogOut?.Flush();
    }
}