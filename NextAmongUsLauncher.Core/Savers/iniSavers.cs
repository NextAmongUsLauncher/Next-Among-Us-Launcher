using System.Text;
using NextAmongUsLauncher.Core.Base;

namespace NextAmongUsLauncher.Core.Savers;

public sealed class iniSavers : DataSaver
{
    public static readonly List<iniSavers> AllIniSaversList = new ();

    public static iniSavers? Get(string fileName, string? filePath = null)
    {
        return AllIniSaversList.Any(n => n.FileName == fileName || n.FilePath == filePath || n.FileName == fileName + ".ini") ? AllIniSaversList.First(_ => Contains()) : new iniSavers(fileName, filePath);

        bool Contains() =>
            AllIniSaversList.Any(n => n.FileName == fileName || n.FilePath == filePath);
    }

    public Dictionary<string, Dictionary<string, string>> Data { get; private set; } = new ();
    
    public override string FileName { get; protected set; }

    public override string FilePath { get; protected set; }

    public override void Save()
    {
        using Stream stream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
        using TextWriter writer = new StreamWriter(stream, Encoding.UTF8);
        foreach (var (Current, varData) in Data)
        {
            writer.WriteLine($"[{Current}]");
            foreach (var (key, value) in varData)
            {
                writer.WriteLine($"{key}={value}");
            }
        }
    }

    public override void Load()
    {
        using Stream stream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Read);
        using TextReader reader = new StreamReader(stream);
        var line = reader.ReadLine();
        var Current = string.Empty;
        while (line != null)
        {
            if (line.Contains('['))
                Current = line.Replace("[", "").Replace("]", "");
            
            if (!line.Contains('[') && Current == string.Empty)
                continue;

            if (!line.Contains('='))
                continue;

            var Key = line.Split("=")[0].Trim();
            var Value = line.Split("=")[1].Trim();

            Data[Current][Key] = Value;
            
            line = reader.ReadLine();
        }
    }

    public void Add(string data, string Key, string Value) =>
        Data[data][Key] = Value;

    public void Remove(string data) => Data.Remove(data);

    public void Remove(string data, string key) => Data[data].Remove(key);

    private iniSavers(string fileName, string? filePath = null)
    {
        if (!fileName.EndsWith(".ini"))
            fileName += ".ini";
        
        FileName = fileName;

        FilePath = filePath ?? Path.Combine(LauncherDirectory.Instance?.CONFIG_PATH!, fileName);
        
        AllIniSaversList.Add(this);
    }
}