namespace NextAmongUsLauncher.Core.Document;

public class YamlLoader
{
    public string File_Path { get; private set; } = "";
    
    public YamlLoader() { }

    public YamlLoader(string filePath)
    {
        File_Path = filePath;
    }
}