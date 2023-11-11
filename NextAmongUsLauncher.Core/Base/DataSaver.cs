namespace NextAmongUsLauncher.Core.Base;

public abstract class DataSaver
{
    public abstract string FileName { get; protected set; }
    
    public abstract string FilePath { get; protected set; }
    
    public abstract bool Save();
    
    public abstract bool Load();
}