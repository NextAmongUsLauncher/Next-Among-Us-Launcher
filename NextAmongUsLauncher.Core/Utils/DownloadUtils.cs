namespace NextAmongUsLauncher.Core.Utils;

public static class DownloadUtils
{
    public static string GetURL(params string[] paths)
    {
        return string.Join("/", paths);
    }
}