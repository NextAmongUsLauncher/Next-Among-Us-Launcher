namespace NextAmongUsLauncher.Core;

public abstract class DataSaver
{
    public abstract string FileName { get; protected set; }
    
    public abstract void Save(string data);
    public abstract string Load();    
}