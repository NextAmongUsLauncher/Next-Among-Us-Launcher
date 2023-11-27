namespace NextAmongUsLauncher.Core.Base;

public abstract class DataSaver
{
    public abstract string FileName { get; protected set; }
    
    public abstract string FilePath { get; protected set; }
    
    public abstract void Save();
    
    public abstract void Load();
}