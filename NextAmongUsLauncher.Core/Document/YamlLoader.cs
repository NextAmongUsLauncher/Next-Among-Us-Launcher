namespace NextAmongUsLauncher.Core.Document;

public class YamlLoader
{
    public YamlLoader()
    {
    }

    public YamlLoader(string filePath)
    {
        File_Path = filePath;
    }

    public string File_Path { get; private set; } = "";
}