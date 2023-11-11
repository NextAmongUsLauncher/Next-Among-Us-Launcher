using System.Security.Cryptography;
using System.Text;

namespace NextAmongUsLauncher.Core.Utils;

public static class HashUtils
{
    private static readonly MD5 md5 = MD5.Create();
    
    public static string GetMD5(string path)
    {
        using var stream = FileUtils.GetStream(path);
        var hashString = GetMD5(stream);

        return hashString;
    }

    public static string GetMD5(Stream stream)
    {
        var buffers = md5.ComputeHash(stream);
        md5.Clear();
        var Builder = new StringBuilder();
        foreach (var buffer in buffers)
        {
            Builder.Append(buffer.ToString("x2"));
        }
        var hashString = Builder.ToString();
        return hashString;
    }
}