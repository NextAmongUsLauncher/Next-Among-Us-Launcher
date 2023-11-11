namespace NextAmongUsLauncher.Core.Utils;

public static class FileUtils
{
    public static Stream GetStream(string path)
    {
        return File.OpenRead(path);        
    }

    public static Stream GetStream(params string[] path)
    {
        return GetStream(Path.Combine(path));
    }
}