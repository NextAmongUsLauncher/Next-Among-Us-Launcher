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

    public static Stream CreateFile(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory) && directory != null)
            Directory.CreateDirectory(directory);

        var stream = !File.Exists(path) ? File.Create(path) : GetStream(path);
        return stream;
    }

    public static string GetPath(this string RootPath, params string[] path)
    {
        return Path.Combine(RootPath, Path.Combine(path));
    }
}